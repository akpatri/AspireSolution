var builder = DistributedApplication.CreateBuilder(args);

// Add services to the container.
var cache = builder.AddRedis("cache")
    .WithRedisCommander();

// Ensure MySQL is started before dependent services
var mySql = builder.AddMySql("mySql")
    .WithPhpMyAdmin();
var db = mySql.AddDatabase("ProjectBDb");


var projectA = builder.AddProject<Projects.ProjectA>("projA")
    .WithReference(cache); //cache will be used by projectA

var projectB = builder.AddProject<Projects.ProjectB>("projB")
    .WithReference(db);
   

var projectC = builder.AddProject<Projects.ProjectC>("projC")
    .WithReference(projectA);

builder.Build().Run();
