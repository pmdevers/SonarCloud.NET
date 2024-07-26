using SonarCloud.NET;
using SonarCloud.NET.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSonarCloudClient(o => {
    o.AccessToken = "<sonarcloud-token-here>";
});

var app = builder.Build();

var client = app.Services.GetRequiredService<ISonarCloudApiClient>();

var result = await client.Projects.Search(new() { 
    Organization = "my-org"
    });

foreach(var project in result.Components) 
{
    Console.WriteLine(project.Name);
}

await app.RunAsync();
