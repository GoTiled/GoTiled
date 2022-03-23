#tool "nuget:?package=NuGet.CommandLine&version=6.0.0"

var semanticVersion = Argument<string>("buildversion");
var version = semanticVersion.Split(new char[] { '-' }).FirstOrDefault() ?? semanticVersion;

Task("Clean")
    .Does(context => 
{
    CleanDirectory("./.artifacts");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(context => 
{
    DotNetBuild("./GoTiled.sln", new DotNetBuildSettings {
        Configuration = "Release",
        NoIncremental = true,
        MSBuildSettings = new DotNetMSBuildSettings()
            .WithProperty("Version", version)
            .WithProperty("AssemblyVersion", version)
            .WithProperty("FileVersion", version)
    });
});

Task("Publish")
    .IsDependentOn("Build")
    .Does(context => 
{
    // Make sure that there is an API key.
    var apiKey =  context.EnvironmentVariable("NUGET_API_KEY");
    if (string.IsNullOrWhiteSpace(apiKey)) {
        throw new CakeException("No NuGet API key specified.");
    }

    // Pack all projects
    context.DotNetPack($"./GoTiled.sln", new DotNetPackSettings {
        Configuration = "Release",
        OutputDirectory = "./.artifacts",
        NoBuild = true,
        MSBuildSettings = new DotNetMSBuildSettings()
            .WithProperty("PackageVersion", semanticVersion)
    });

    // Publish all projects
    foreach(var file in GetFiles("./.artifacts/*.nupkg"))
    {
        context.Information("Publishing {0}...", file.FullPath);
        context.NuGetPush(file, new NuGetPushSettings {
            ApiKey = apiKey,
            Source = "https://api.nuget.org/v3/index.json"
        });
    }
});

RunTarget(Argument("target", "Build"))
