var builder = DistributedApplication.CreateBuilder(args);


//Core Postgres Db & PgAdmin
var postgresUser = builder.AddParameter("core-monolith-db-username", secret: false);
var postgresPassword = builder.AddParameter("core-monolith-db-password", secret: true);

var corePgAdminName = builder.Configuration["AppConfig:CorePgAdminName"];
var corePostgresName = builder.Configuration["AppConfig:CorePostgresName"];
var corePostgresDbName = builder.Configuration["AppConfig:CorePostgresDbName"];

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


//Core RabbitMQ
var coreMqName = builder.Configuration["AppConfig:CoreRabbitMqName"];

var rabbitMq = builder.AddRabbitMQ($"{coreMqName}")
    .WithVolume($"{coreMqName}-volume", @"/var/lib/rabbitmq")
    .WithManagementPlugin()
    .WithLifetime(ContainerLifetime.Persistent);


//Core Redis
var coreRedisName = builder.Configuration["AppConfig:CoreRedisName"];

var redis = builder.AddRedis($"{coreRedisName}");


//Core WebApi
var coreWebApiEnv = builder.AddParameter("core-monolith-webapi-env", secret: false);

var coreWebApiName = builder.Configuration["AppConfig:CoreWebApiName"];

var webApi = builder.AddProject<Projects.CoreMonolith_WebApi>($"{coreWebApiName}")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", coreWebApiEnv)
    .WithExternalHttpEndpoints()
    .WithReference(postgressDb)
    .WaitFor(postgres)
    .WaitFor(postgressDb)
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq);


//DownloadManager WebApp
var downloadManagerEnv = builder.AddParameter("download-manager-webapp-env", secret: false);

var downloadManagerWebAppName = builder.Configuration["AppConfig:DownloadManagerWebAppName"];

builder.AddProject<Projects.DownloadManager_WebApp>($"{downloadManagerWebAppName}")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", downloadManagerEnv)
    .WithExternalHttpEndpoints()
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(webApi)
    .WaitFor(webApi);

await builder.Build().RunAsync();
