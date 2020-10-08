# SecretHub Client for .NET <sup>[BETA](#beta)</a></sup>

This repository provides a .NET client for the SecretHub Secrets Management API. 

> [SecretHub](https://secrethub.io) is a secrets management tool that works for every engineer and allows you to securely provision passwords and keys throughout your entire stack with just a few lines of code.

## Table of Contents
 - [Installation](#installation)
 - [Usage](#usage)
 - [Getting help](#getting-help)
 - [BETA](#beta)
 - [Developing](#developing)

## Installation

To install the SecretHub package from NuGet Gallery, run the following command in your project's directory: 

```bash
dotnet add package SecretHub --version 0.2.0
```

Or you can add the following line to your project's `csproj` file:

```xml
<PackageReference Include="SecretHub" Version="0.2.0" />
```

The package supports Linux and Windows for 32-bit and 64-bit architectures and works with both .NET Core and the full .NET Framework. 

Make sure you have create a SecretHub account and set up a credential on your system before using the library. See the [Credential](#credential) section for more info. 

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
Dictionary<string, string>
{
    {"MY_SECRET", "the value of the secret path/to/secret"},
    {"OTHER_VARIABLE", "some-other-value"}
}
```

### `ExportEnv(IDictionary<string, string> env)`
Adds the environment variables defined in the `env` dictionary to the environment of the process.
If any of them are already present in the environment, they will be overwritten.

This method can be used together with `ResolveEnv` to resolve all secret references in the environment:
```csharp
client.ExportEnv(client.ResolveEnv());
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

### Credential
To use the SecretHub .NET client, you need to provide a credential for your __SecretHub__ account.
You can sign up for a free developer account [here](https://signup.secrethub.io/).

After signup, the credential is located at `$HOME/.secrethub/credential` by default.
`secrethub.NewClient()` automatically uses this credential.

You can also provide a credential through the `SECRETHUB_CREDENTIAL` environment variable.

## Getting Help

Come chat with us on [Discord](https://discord.gg/EQcE87s) or email us at [support@secrethub.io](mailto:support@secrethub.io)

## BETA
This project is currently in beta and we'd love your feedback! Check out the [issues](https://github.com/secrethub/secrethub-dotnet/issues?q=is%3Aissue+is%3Aopen+sort%3Aupdated-desc) and feel free suggest cool ideas, use cases, or improvements.

Because it's still in beta, you can expect to see some changes introduced. Pull requests are very welcome.

For support, send us a message on [Discord](https://discord.gg/wcxV5RD) or send an email to support@secrethub.io

## Developing

Note that most of the code in this repository is automatically generated from the [SecretHub XGO project](https://github.com/secrethub/secrethub-xgo), which wraps the `secrethub-go` client with `cgo` exported functions so it can be called form other languages, e.g. C, C#, Python, Ruby, NodeJS, and Java. To generate the code [SWIG](http://www.swig.org/) is used. 

See the [SecretHub XGO repository](https://github.com/secrethub/secrethub-xgo) for more details.

### Building from source 
1. Make sure you have [Golang](https://golang.org/doc/install) installed.
1. Execute `make nupkg` from the Makefile
1. Go to your .NET project directory and run the following command: `dotnet add package SecretHub -s <path_to_your_secrethub-xgo_repo>`.
