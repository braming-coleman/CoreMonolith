var builder = DistributedApplication.CreateBuilder(args);

var coreWebApiEnv = builder.AddParameter("core-monolith-webapi-env", secret: false);
var postgresUser = builder.AddParameter("core-monolith-db-username", secret: false);
var postgresPassword = builder.AddParameter("core-monolith-db-password", secret: true);

var corePostgresName = builder.Configuration["AppConfig:CorePostgresName"];
var corePostgresDbName = builder.Configuration["AppConfig:CorePostgresDbName"];
var coreMqName = builder.Configuration["AppConfig:CoreRabbitMqName"];
var coreWebApiName = builder.Configuration["AppConfig:CoreWebApiName"];

var postgres = builder.AddPostgres($"{corePostgresName}", postgresUser, postgresPassword)
    .WithVolume($"{corePostgresName}-volume", @"/var/lib/postgresql/data")
    .WithEnvironment("POSTGRES_DB", corePostgresDbName)
    .WithLifetime(ContainerLifetime.Persistent);

postgres.WithPgAdmin();

var postgressDb = postgres.AddDatabase($"{corePostgresDbName}");

var rabbitMq = builder.AddRabbitMQ($"{coreMqName}")
    .WithVolume($"{coreMqName}-volume", @"/var/lib/rabbitmq")
    .WithManagementPlugin()
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.CoreMonolith_WebApi>($"{coreWebApiName}")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", coreWebApiEnv)
    .WithReference(postgressDb)
    .WithReference(rabbitMq)
    .WaitFor(postgressDb);

await builder.Build().RunAsync();
