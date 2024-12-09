var builder = DistributedApplication.CreateBuilder(args);

var corePostgresName = builder.Configuration["AppConfig:CorePostgresName"];
var coreMqName = builder.Configuration["AppConfig:CoreRabbitMqName"];
var coreWebApiName = builder.Configuration["AppConfig:CoreWebApiName"];

var baseDataVolumePath = builder.Configuration["Containers:DataVolumePath"];

var postgres = builder.AddPostgres($"{corePostgresName}")
    .WithDataBindMount(@$"{baseDataVolumePath}\{corePostgresName}")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

var rabbitMq = builder.AddRabbitMQ($"{coreMqName}")
    .WithDataBindMount(@$"{baseDataVolumePath}\{coreMqName}")
    .WithManagementPlugin()
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.CoreMonolith_WebApi>($"{coreWebApiName}")
    .WithReference(postgres)
    .WithReference(rabbitMq)
    .WaitFor(postgres);

builder.Build().Run();
