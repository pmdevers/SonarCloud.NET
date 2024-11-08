# SonarCloud API Client for .NET Core

![Github Release](https://img.shields.io/github/v/release/pmdevers/SonarCloud.NET) 
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/pmdevers/SonarCloud.NET/.github%2Fworkflows%2Fbuild-publish.yml) 
![GitHub License](https://img.shields.io/github/license/pmdevers/SonarCloud.NET) 
![Github Issues Open](https://img.shields.io/github/issues/pmdevers/SonarCloud.NET) 
![Github Pull Request Open](https://img.shields.io/github/issues-pr/pmdevers/SonarCloud.NET) 
[![Scheduled Code Security Testing](https://github.com/pmdevers/SonarCloud.NET/actions/workflows/security-analysis.yml/badge.svg?event=schedule)](https://github.com/pmdevers/SonarCloud.NET/actions/workflows/security-analysis.yml)



This repository contains a .NET Core library for interacting with the SonarCloud API. The library wraps the API endpoints and provides a strongly-typed, easy-to-use interface for making API calls from your .NET applications.

![Alt text](https://raw.githubusercontent.com/pmdevers/SonarCloud.NET/master/assets/sonar-dark.png "SonarCloud logo")

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)

## Features

- Easy integration with SonarCloud API
- Strongly-typed responses
- Supports .NET Core applications
- Asynchronous API calls
- Error handling and logging

## Installation

To install the package, use the following command in your .NET Core project:

```bash
dotnet add package PMDEvers.SonarCloud.NET
```

Alternatively, you can add it manually to your `.csproj` file:

```xml
<PackageReference Include="PMDEvers.SonarCloud.NET" Version="0.1.0" />
```

## Usage

Here are some basic examples of how to use the library:

### Initializing the Client

First, initialize the `SonarCloudClient` with your SonarCloud token:

```csharp
using SonarCloud.NET;
using SonarCloud.NET.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSonarCloudClient(o => {
    o.AccessToken = "<sonarcloud-token-here>";
});

```

### Listing Projects

To fetch information about a specific project:

```csharp
var result = await client.Projects.Search(new() { 
    Organization = "my-org"
    });

foreach(var project in result.Components) 
{
    Console.WriteLine(project.Name);
}
```

### For console applications

If you're using writing a script or console application:

```
public static async Task Main(string[] args) 
{
    var sc = new ServiceCollection();
    sc.AddSonarCloudClient(cfg => cfg.AccessToken = MY_API_KEY);
    sc.AddLogging();
    var provider = sc.BuildServiceProvider();
    var sonarClient = provider.GetService<ISonarCloudApiClient>();
}
```

## Configuration

### Authentication

You must provide a SonarCloud token to authenticate your requests. You can generate a token from your SonarCloud account under the Security section.

### Handling API Limits

Be mindful of the API rate limits imposed by SonarCloud. The client includes built-in handling for rate limit responses, but you should design your application to respect these limits and implement retry logic as needed.

## Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue if you encounter any bugs or have feature requests.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/fooBar`)
3. Commit your changes (`git commit -am 'Add some fooBar'`)
4. Push to the branch (`git push origin feature/fooBar`)
5. Create a new Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.md) file for details.
