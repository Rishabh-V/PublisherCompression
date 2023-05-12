namespace netstd Google.Cloud.PubSub.Compression.Thrift

enum PhoneType {
  MOBILE = 0,
  HOME = 1,
  WORK = 2
}

struct Name {
  1: string firstName,
  2: string lastName
}

struct Phone {
  1: PhoneType type = PhoneType.MOBILE,
  2: i32       number
}

struct Person {
  1: Name        name,
  2: list<Phone> phones,
}

struct PhoneBook {
  1: list<Person> people,
}