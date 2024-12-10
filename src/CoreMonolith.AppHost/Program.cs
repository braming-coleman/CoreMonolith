var builder = DistributedApplication.CreateBuilder(args);

var coreWebApiEnv = builder.AddParameter("core-monolith-webapi-env");
var postgresUser = builder.AddParameter("core-monolith-db-username");
var postgresPassword = builder.AddParameter("core-monolith-db-password");

var corePostgresName = builder.Configuration["AppConfig:CorePostgresName"];
var corePostgresDbName = builder.Configuration["AppConfig:CorePostgresDbName"];
var coreMqName = builder.Configuration["AppConfig:CoreRabbitMqName"];
var coreWebApiName = builder.Configuration["AppConfig:CoreWebApiName"];

var postgres = builder.AddPostgres($"{corePostgresName}", postgresUser, postgresPassword)
    .WithVolume($"{corePostgresName}-volume", @"/var/lib/postgresql/data")
    .WithEnvironment("POSTGRES_DB", corePostgresDbName)
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase($"{corePostgresDbName}");

var rabbitMq = builder.AddRabbitMQ($"{coreMqName}")
    .WithVolume($"{coreMqName}-volume", @"/var/lib/rabbitmq")
    .WithManagementPlugin()
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.CoreMonolith_WebApi>($"{coreWebApiName}")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", coreWebApiEnv)
    .WithReference(postgres)
    .WithReference(rabbitMq)
    .WaitFor(postgres);

await builder.Build().RunAsync();
