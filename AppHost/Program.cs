var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ProjectA>("projA");
builder.AddProject<Projects.ProjectB>("projB");
builder.AddProject<Projects.ProjectC>("projC"); 




builder.Build().Run();
