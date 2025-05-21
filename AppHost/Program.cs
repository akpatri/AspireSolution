var builder = DistributedApplication.CreateBuilder(args);

var projectA = builder.AddProject<Projects.ProjectA>("projA");

var projectB = builder.AddProject<Projects.ProjectB>("projB");
var projectC = builder.AddProject<Projects.ProjectC>("projC")
    .WithReference(projectA);




builder.Build().Run();
