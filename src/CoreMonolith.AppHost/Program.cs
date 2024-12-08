var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("core-monolith-db")
    .WithDataVolume()
    .WithPgAdmin();

//var coreMonolithdb = postgres.AddDatabase("core-monolith");

var rabbitMq = builder.AddRabbitMQ("core-monolith-mq")
    .WithManagementPlugin();

builder.AddProject<Projects.CoreMonolith_WebApi>("core-monolith-webapi")
    .WithReference(postgres)
    .WithReference(rabbitMq);

builder.Build().Run();

