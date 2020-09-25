# SecretHub Client for .NET

SecretHub - XGO wraps the `secrethub-go` client with `cgo` exported functions so it can be called form other languages, e.g. C, C#, Python, Ruby, NodeJS, and Java. To generate the code that will then be wrapped in the library used by a certian programming language, we use `swig`.

## Table of Contents
 - [Prerequisites](#prerequisites)
 - [Installation](#installation)
 - [Building from source](#building-from-source)
 - [Usage](#usage)
 - [Resources](#resources)
 - [Getting help](#getting-help)

## Prerequisites
To make use of the package, you will need to have the following installed:
 - [.NET Core](https://docs.microsoft.com/en-gb/dotnet/core/install/)

## Installation

To install SecretHub package from NuGet Gallery, run the following command in your project's directory: 
```bash
dotnet add package SecretHub --version 0.1.0
```
or you can add the following line to your project's `csproj` file:
```xml
<PackageReference Include="SecretHub" Version="0.1.0" />
```

## Building from source 
1. Make sure you have [Golang](https://golang.org/doc/install) installed.
1. Execute `make nupkg` from the Makefile
1. Go to your .NET project direcotry and run the following command: `dotnet add package SecretHub -s <path_to_your_secrethub-xgo_repo>`.

## Usage
Before doing any calls to the library, you need to create you SecretHub client:
```csharp
var client = new SecretHub.Client();
```

After you have your client, you can call the following methods:

### `Read(string path)`
Retrieve a secret, including all its metadata.
```csharp
SecretHub.SecretVersion secret = client.Read("path/to/secret");
Console.WriteLine("The secret value is " + secret.Data);
```
`SecretHub.SecretVersion` object represents a version of a secret with sensitive data.

### `ReadString(string path)`
Retrieve a secret as a string.
 ```csharp
 string secret = client.Read("path/to/secret");
 Console.WriteLine("The secret value is " + secret);
 ```

### `Exists(string path)`
Check if a secret exists at `path`.
```csharp
bool secretExists = client.Exists("path/to/secret");
```

### `Write(string path, string secret)`
Write a secret to a given `path`.
```csharp
client.Write("path/to/secret", "secret_value");
```

### `Remove(string path)`
Delete the secret found at `path`.
```csharp
client.Remove("path/to/secret");
```

### `Resolve(string ref)`
Fetch the value of a secret from SecretHub, when the `ref` has the format `secrethub://<path>`, otherwise it returns `ref` unchanged.
```csharp
string resolvedRef = client.Resolve("secrethub://path/to/secret");
Console.WriteLine("The secret value got from reference is " + resolvedRef);
```

### `ResolveEnv()`
Return a dictionary containing the OS environment with all secret references (`secrethub://<path>`) replaced by their corresponding secret values.

For example, if the following two environment variables are set:
 - `MY_SECRET=secrethub://path/to/secret`
 - `OTHER_VARIABLE=some-other-value`

Then the following call to `ResolveEnv()`
```csharp
Dictionary<string, string> resolvedEnv = client.ResolveEnv();
```

would lead to the `resolvedEnv` containing the following contents:
```csharp
Doctionary<string, string>
{
    {"MY_SECRET", "the value of the secret path/to/secret"},
    {"OTHER_VARIABLE", "some-other-value"}
}
```

### Exceptions
Any error encountered by the SecretHub client will be thrown as an `ApplicationException`. The full error message can be retrieved from the `Message` field.
```csharp
try 
{
	string secret = client.Read("path/to/secret");
} 
catch(ApplicationException ex)
{
	Console.WriteLine(ex.Message);
}
```

## Resources

For example code and more reading on the specific tech behind this cross language magic, see these resources:

- https://medium.com/learning-the-go-programming-language/calling-go-functions-from-other-languages-4c7d8bcc69bf
- https://github.com/vladimirvivien/go-cshared-examples/
- https://github.com/draffensperger/go-interlang
- https://golang.org/cmd/cgo/
- http://www.swig.org/

## Getting Help

Come chat with us on [Discord](https://discord.gg/EQcE87s) or email us at [support@secrethub.io](mailto:support@secrethub.io)
