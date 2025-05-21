var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
    .WithRedisCommander();
var projectA = builder.AddProject<Projects.ProjectA>("projA")
    .WithReference(cache);

var projectB = builder.AddProject<Projects.ProjectB>("projB");
var projectC = builder.AddProject<Projects.ProjectC>("projC")
    .WithReference(projectA);




builder.Build().Run();
