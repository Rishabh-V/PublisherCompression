using Google.Cloud.PubSub.Compression.Thrift;
using System.Text;

namespace PublisherCompression.DataGenerator;

public class ThriftObjectGenerator
{
    private readonly Random _random;

    public ThriftObjectGenerator()
    {
        _random = new Random();
    }

    public Space GenerateSpace()
    {
        Space space = new()
        {
            Profile = GenerateProfile(),
            HomePage = GenerateHomePage(),
            Timeline = GenerateTimeline(),
            Followees = new List<Profile>(),
            Followers = new List<Profile>()
        };

        for (int i = 0; i < 2; i++)
        {
            space.Followers.Add(GenerateProfile());
            space.Followees.Add(GenerateProfile());
        }

        return space;
    }

    private HomePage GenerateHomePage()
    {
        HomePage homePage = new()
        {
            Profile = GenerateProfile(),
            ProfileTweets = new List<Tweet>()
        };

        for (int i = 0; i < 5; i++)
        {
            homePage.ProfileTweets.Add(GenerateTweet());
        }
        return homePage;
    }

    private Timeline GenerateTimeline()
    {
        Timeline timeline = new()
        {
            ProfileTweets = new List<Tweet>(),
            FollowerTweets = new List<Tweet>(),
            FolloweeTweets = new List<Tweet>()
        };

        for (int i = 0; i < 5; i++)
        {
            timeline.ProfileTweets.Add(GenerateTweet());
            timeline.FollowerTweets.Add(GenerateTweet());
            timeline.FolloweeTweets.Add(GenerateTweet());
        }
        return timeline;
    }

    private Tweet GenerateTweet()
    {
        Tweet tweet = new()
        {
            Profile = GenerateProfile(),
            Text = GenerateRandomString(),
            Loc = GenerateLocation()
        };

        return tweet;
    }

    private Profile GenerateProfile()
    {
        Profile profile = new()
        {
            UserId = GenerateRandomInt(),
            Person = GeneratePerson(),
            Bio = GenerateRandomString(),
            Hometown = GenerateAddress(),
            Hobby = GenerateRandomString(),
            Dob = GenerateRandomString(),
            Occupation = GenerateRandomString(),
            Jobs = new List<Job>(),
            Educations = new List<Education>()
        };

        for (int i = 0; i < 3; i++)
        {
            profile.Jobs.Add(GenerateJob());
        }

        profile.Educations.Add(GenerateEducation());
        return profile;
    }

    public Person GeneratePerson()
    {
        Person person = new()
        {
            Name = GenerateName(),
            Phones = new List<Phone>()
        };

        for (int i = 0; i < 2; i++)
        {
            person.Phones.Add(GeneratePhone());
        }

        return person;
    }

    private Location GenerateLocation()
    {
        Location location = new()
        {
            Latitude = GenerateRandomDouble(),
            Longitude = GenerateRandomDouble()
        };

        return location;
    }

    private Name GenerateName()
    {
        Name name = new()
        {
            FirstName = GenerateRandomString(),
            LastName = GenerateRandomString()
        };

        return name;
    }

    private Phone GeneratePhone()
    {
        Phone phone = new Phone
        {
            Number = GenerateRandomInt()
        };
        return phone;
    }

    private Address GenerateAddress()
    {
        Address address = new()
        {
            Street = GenerateRandomString(),
            Apartment = GenerateRandomString(),
            City = GenerateRandomString(),
            State = GenerateRandomString(),
            Country = GenerateRandomString(),
            ZipCode = GenerateRandomInt(),
            Location = GenerateLocation()
        };

        return address;
    }

    private Company GenerateCompany()
    {
        Company company = new()
        {
            Name = GenerateRandomString(),
            HeadQuarter = GenerateAddress(),
            EstablishDate = GenerateRandomString(),
            Offices = new List<Address>()
        };

        for (int i = 0; i < 5; i++)
        {
            company.Offices.Add(GenerateAddress());
        }

        return company;
    }

    private Job GenerateJob()
    {
        var job = new Job
        {
            Company = GenerateCompany(),
            Address = GenerateAddress(),
            Designation = GenerateRandomString(),
            EndDate = GenerateRandomDate(),
            StartDate = GenerateRandomDate()
        };

        return job;
    }

    private Institute GenerateInstitute()
    {
        var institute = new Institute
        {
            Name = GenerateRandomString(),
            StudentCount = GenerateRandomInt(),
            Director = GenerateRandomString(),
            Address = GenerateAddress(),
            EstablishDate = GenerateRandomDate()
        };

        return institute;
    }

    private Education GenerateEducation()
    {
        var education = new Education
        {
            StartDate = GenerateRandomDate(),
            EndDate = GenerateRandomDate(),
            Institute = GenerateInstitute(),
            Degree = GenerateRandomString(),
            Gpa = GenerateRandomDouble(),
            Major = GenerateRandomString()
        };

        return education;
    }

    private string GenerateRandomDate(int minYear = 1900, int maxYear = 2023)
    {
        var randomDate = GenerateRandomInt(1, 31);
        var randomYear = GenerateRandomInt(minYear, maxYear);
        var randomMonth = GenerateRandomInt(1, 12);

        var lessThan31daysMonth = new List<int>() { 2, 4, 6, 7, 9, 11 };

        if ((randomMonth == 2 && randomDate > 28) || (randomDate == 31 && lessThan31daysMonth.Contains(randomMonth)))
        {
            randomDate = GenerateRandomInt(1, 28);
        }

        return new DateTime(randomYear, randomMonth, randomDate).ToString();
    }

    private string GenerateRandomString()
    {
        byte[] array = new byte[7];
        _random.NextBytes(array);
        return Encoding.UTF8.GetString(array, 0, array.Length);
    }

    private int GenerateRandomInt(int min = 100000, int max = 10000000) => min + _random.Next(max - min + 1);

    private double GenerateRandomDouble(double min = 1.0, double max = 10.0) => min + _random.NextDouble() * (max - min);
}
