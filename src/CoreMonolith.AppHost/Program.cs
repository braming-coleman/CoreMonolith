var builder = DistributedApplication.CreateBuilder(args);

var coreWebApiEnv = builder.AddParameter("core-monolith-webapi-env");
var postgresUser = builder.AddParameter("core-monolith-db-username");
var postgresPassword = builder.AddParameter("core-monolith-db-password");

var corePostgresName = builder.Configuration["AppConfig:CorePostgresName"];
var corePostgresDbName = builder.Configuration["AppConfig:CorePostgresDbName"];
var coreMqName = builder.Configuration["AppConfig:CoreRabbitMqName"];
var coreWebApiName = builder.Configuration["AppConfig:CoreWebApiName"];
var baseDataVolumePath = builder.Configuration["Containers:DataVolumePath"];

var postgres = builder.AddPostgres($"{corePostgresName}", postgresUser, postgresPassword)
    .WithDataBindMount(@$"{baseDataVolumePath}\{corePostgresName}")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase($"{corePostgresDbName}");

var rabbitMq = builder.AddRabbitMQ($"{coreMqName}")
    .WithDataBindMount(@$"{baseDataVolumePath}\{coreMqName}")
    .WithManagementPlugin()
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.CoreMonolith_WebApi>($"{coreWebApiName}")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", coreWebApiEnv)
    .WithReference(postgres)
    .WithReference(rabbitMq)
    .WaitFor(postgres);

await builder.Build().RunAsync();
