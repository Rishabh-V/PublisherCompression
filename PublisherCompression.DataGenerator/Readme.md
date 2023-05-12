# Data generator for Publisher client compression testing
This is a simple data generator for the Publisher client compression testing.
The data may not be of exact size but given that the same data is used for all the tests, the scaling factor should be the same for all the tests.

## Usage
- Clone the repository.
- Open the Solution in Visual Studio and build the project.
- Run the application and it should output the data files in the format 
{{MessageType}}_ _{{MessagePattern}}_ _{{Size}}.txt from 100 bytes to ~3.4 MB in the same folder as the executable.
where:
    - `MessageType` is `Synthetic` or `Realistic`
    - `MessagePattern` is `Repeated` or `SemiRandom` or `Random`

## A note on Thrift
(This is mostly to myself, so that I don't forget the command)

We use Thrift to generate the Twitter like data files. We have two Thrift files, one for the Phonebook and one for the Twitter data.
We generate the data files using the Thrift compiler.

The Thrift files are generated using the following command:
```
thrift --gen netstd Phonebook.thrift
thrift --gen netstd Twitter.thrift
```

This generates the C# files in the `gen-netstd` folder. We then input these files as test input for the Test project.