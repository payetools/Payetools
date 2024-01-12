// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.MinVer;
using Nuke.Common.Tools.Slack;
using Nuke.Common.Utilities.Collections;
using System;
using System.Linq;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.Slack.SlackTasks;

[GitHubActions(
    "continuous",
    GitHubActionsImage.UbuntuLatest,
    On = new[] { GitHubActionsTrigger.Push },
    InvokedTargets = new[] { nameof(Publish) },
    ImportSecrets = new[] { nameof(NugetApiKey) },
    FetchDepth = 0)]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter]
    readonly string NugetApiUrl = "https://api.nuget.org/v3/index.json"; //default

    [Parameter]
    [Secret]
    readonly string NugetApiKey;

    [Parameter]
    [Secret]
    readonly string SlackWebhook;

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution]
    readonly Solution Solution;

    [GitRepository]
    readonly GitRepository GitRepository;

    [MinVer]
    readonly MinVer MinVer;

    AbsolutePath SourceDirectory => RootDirectory / "src";

    AbsolutePath TestDirectory => RootDirectory / "test";

    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    Target Status => _ => _
        .Executes(() =>
        {
            Console.WriteLine(GitRepository.Tags.Any());
            Console.WriteLine(GitRepository.IsOnMainOrMasterBranch());
        });

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
            TestDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetFilter("FullyQualifiedName!~Payetools.Testing")
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target Pack => _ => _
        .DependsOn(Test)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetProject(Solution)
                .SetConfiguration(Configuration)
                .SetIncludeSymbols(true)
                .SetOutputDirectory(ArtifactsDirectory)
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target Publish => _ => _
        .OnlyWhenDynamic(() => GitRepository.IsOnMainOrMasterBranch() && GitRepository.Tags.Any())
        .Requires(() => NugetApiKey)
        .Requires(() => NugetApiUrl)
        .DependsOn(Pack)
        .Executes(() =>
        {
            var packageFiles = ArtifactsDirectory
                .GlobFiles("*.nupkg")
                .Where(x => !x.ToString().EndsWith("symbols.nupkg") && !x.ToString().Contains("Payetools.Testing"))
                .ToList();

            packageFiles.ForEach(p =>
                DotNetNuGetPush(s => s
                    .SetTargetPath(p)
                    .SetSource(NugetApiUrl)
                    .SetApiKey(NugetApiKey)
                ));
        });

    Target NotifyPublished => _ => _
        .Requires(() => SlackWebhook)
        .TriggeredBy(Publish)
        .Executes(async () => 
        {
            string message;

            if (GitRepository.Tags.Any())
            {
                message = $"Payetools version {GitRepository.Tags.First()} deployed to Nuget";
            }
            else 
            {
                message = "Payetools not published; no tag supplied";
            }

            await SendSlackMessageAsync(_ => _
                    .SetText(message),
                "https://hooks.slack.com/services/" + SlackWebhook);
        });
}
