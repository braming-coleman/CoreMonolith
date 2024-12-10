var builder = DistributedApplication.CreateBuilder(args);

var coreWebApiEnv = builder.AddParameter("core-monolith-webapi-env", secret: false);
var postgresUser = builder.AddParameter("core-monolith-db-username", secret: false);
var postgresPassword = builder.AddParameter("core-monolith-db-password", secret: true);

var corePostgresName = builder.Configuration["AppConfig:CorePostgresName"];
var corePostgresDbName = builder.Configuration["AppConfig:CorePostgresDbName"];
var corePgAdminName = builder.Configuration["AppConfig:CorePgAdminName"];
var coreRedisName = builder.Configuration["AppConfig:CoreRedisName"];
var coreMqName = builder.Configuration["AppConfig:CoreRabbitMqName"];
var coreWebApiName = builder.Configuration["AppConfig:CoreWebApiName"];

var pgadminHostPort = int.TryParse(builder.Configuration["AppConfig:CorePgAdminHostPort"], out int value) ? value : 80;

var postgres = builder.AddPostgres($"{corePostgresName}", postgresUser, postgresPassword)
    .WithVolume($"{corePostgresName}-volume", @"/var/lib/postgresql/data")
    .WithEnvironment("POSTGRES_DB", corePostgresDbName)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin(config =>
    {
        config
        .PublishAsContainer()
        .WithContainerName($"{corePgAdminName}")
        .WithVolume($"{corePgAdminName}-volume", @"/var/lib/pgadmin")
        .WithHostPort(pgadminHostPort)
        .WithLifetime(ContainerLifetime.Persistent);
    });

var postgressDb = postgres.AddDatabase($"{corePostgresDbName}");

var rabbitMq = builder.AddRabbitMQ($"{coreMqName}")
    .WithVolume($"{coreMqName}-volume", @"/var/lib/rabbitmq")
    .WithManagementPlugin()
    .WithLifetime(ContainerLifetime.Persistent);

var redis = builder.AddRedis($"{coreRedisName}");

builder.AddProject<Projects.CoreMonolith_WebApi>($"{coreWebApiName}")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", coreWebApiEnv)
    .WithReference(postgressDb)
    .WithReference(redis)
    .WithReference(rabbitMq)
    .WaitFor(postgres)
    .WaitFor(postgressDb)
    .WaitFor(redis);

await builder.Build().RunAsync();
