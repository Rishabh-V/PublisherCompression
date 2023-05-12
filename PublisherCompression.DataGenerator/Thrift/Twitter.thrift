namespace netstd Google.Cloud.PubSub.Compression.Thrift

include "phonebook.thrift"

struct Location {
    1: double latitude,
    2: double longitude,
}

struct Address {
    1: string   street,
    2: string   apartment,
    3: string   city,
    4: string   state,
    5: string   country,
    6: i32      zipCode,
    7: Location location,
}

struct Company {
    1: string                 name,
    2: Address                headQuarter,
    3: list<Address>          offices,
    4: string                 establishDate,
    5: string                 description,
    6: i32                    employeeCount,
    7: list<phonebook.Person> founders,
}

struct Job {
    1: Company company,
    2: string  designation,
    3: string  startDate,
    4: string  endDate,
    5: Address address,
}

struct Institute {
    1: string  name,
    2: Address address,
    3: string  establishDate,
    4: i32     studentCount,
    5: string  director
}

struct Education {
    1: Institute institute,
    2: string    startDate,
    3: string    endDate,
    4: string    major,
    5: string    degree,
    6: double    gpa,
}

struct Profile {
    1: i32              userId,
    2: phonebook.Person person,
    3: string           bio,
    4: Address          hometown,
    5: string           hobby,
    6: string           dob,
    7: string           occupation,
    8: list<Job>        jobs,
    9: list<Education>  educations,
}

typedef list<Profile> ProfileList

enum TweetType {
    TWEET,
    RETWEET = 2,
    DM = 0xa,
    REPLY
}

struct Tweet {
    1: Profile   profile,
    2: string    text,
    3: Location  loc,
    4: TweetType tweetType = TweetType.TWEET,
    5: string    language = "english",
}

typedef list<Tweet> TweetList

struct Timeline {
    1: TweetList profileTweets,
    2: TweetList followerTweets,
    3: TweetList followeeTweets,
}

struct HomePage {
    1: Profile    profile,
    2: TweetList  profileTweets,
}

struct Space {
    1: Profile     profile,
    2: HomePage    homePage,
    3: Timeline    timeline,
    4: ProfileList followers,
    5: ProfileList followees,
}

struct Twitter {
    1: list<Space> spaces,
}