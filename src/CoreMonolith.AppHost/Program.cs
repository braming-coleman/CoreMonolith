var builder = DistributedApplication.CreateBuilder(args);

var postgresUser = builder.AddParameter("core-monolith-db-username");
var postgresPassword = builder.AddParameter("core-monolith-db-password");

var corePostgresName = builder.Configuration["AppConfig:CorePostgresName"];
var coreMqName = builder.Configuration["AppConfig:CoreRabbitMqName"];
var coreWebApiName = builder.Configuration["AppConfig:CoreWebApiName"];
var baseDataVolumePath = builder.Configuration["Containers:DataVolumePath"];

var postgres = builder.AddPostgres($"{corePostgresName}", postgresUser, postgresPassword)
    .WithDataBindMount(@$"{baseDataVolumePath}\{corePostgresName}")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

var rabbitMq = builder.AddRabbitMQ($"{coreMqName}")
    .WithDataBindMount(@$"{baseDataVolumePath}\{coreMqName}")
    .WithManagementPlugin()
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.CoreMonolith_WebApi>($"{coreWebApiName}")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", builder.Environment.EnvironmentName)
    .WithReference(postgres)
    .WithReference(rabbitMq)
    .WaitFor(postgres);

await builder.Build().RunAsync();
