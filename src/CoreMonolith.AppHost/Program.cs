using CoreMonolith.ServiceDefaults.Constants;

var builder = DistributedApplication.CreateBuilder(args);

//Core Postgres Db & PgAdmin
var postgresUser = builder.AddParameter(ConfigKeyConstants.DbUsernameKeyName, secret: false);
var postgresPassword = builder.AddParameter(ConfigKeyConstants.DbPasswordKeyName, secret: true);

var pgadminHostPort = int.TryParse(builder.Configuration["AppConfig:CorePgAdminHostPort"], out int value) ? value : 80;

var postgres = builder.AddPostgres(ResourceNameConstants.DbServerName, postgresUser, postgresPassword)
    .WithVolume($"{ResourceNameConstants.DbServerName}-volume", @"/var/lib/postgresql/data")
    .WithEnvironment("POSTGRES_DB", ConnectionNameConstants.DbConnStringName)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin(config =>
    {
        config
        .PublishAsContainer()
        .WithContainerName(ResourceNameConstants.DbServerAdminName)
        .WithVolume($"{ResourceNameConstants.DbServerAdminName}-volume", @"/var/lib/pgadmin")
        .WithHostPort(pgadminHostPort)
        .WithExternalHttpEndpoints()
        .WithLifetime(ContainerLifetime.Persistent);
    });

var postgressDb = postgres.AddDatabase(ConnectionNameConstants.DbConnStringName);
//-----------------------------------------------------------------------------------------//


//Core RabbitMQ
var rabbitMq = builder.AddRabbitMQ(ConnectionNameConstants.RabbitMqConnectionName)
    .WithVolume($"{ConnectionNameConstants.RabbitMqConnectionName}-volume", @"/var/lib/rabbitmq")
    .WithManagementPlugin()
    .WithExternalHttpEndpoints()
    .WithLifetime(ContainerLifetime.Persistent);
//-----------------------------------------------------------------------------------------//


//Core Redis
var redis = builder.AddRedis(ConnectionNameConstants.RedisConnectionName)
    .WithExternalHttpEndpoints()
    .WithRedisCommander(config =>
    {
        config
        .PublishAsContainer()
        .WithContainerName(ResourceNameConstants.RedisAdminName)
        .WithExternalHttpEndpoints()
        .WithLifetime(ContainerLifetime.Persistent);
    });
//-----------------------------------------------------------------------------------------//


//Keycloak
var keycloak = builder.AddKeycloak(ConnectionNameConstants.KeycloakConnectionName)
    .WithVolume($"{ConnectionNameConstants.KeycloakConnectionName}-volume", @"/opt/keycloak")
    .WithExternalHttpEndpoints()
    .WithLifetime(ContainerLifetime.Persistent);
//-----------------------------------------------------------------------------------------//


//Core WebApi
var jwtSecret = builder.AddParameter(ConfigKeyConstants.JwtSecretKeyName, secret: true);
var coreWebApiEnv = builder.AddParameter(ConfigKeyConstants.WebApiEnvKeyName, secret: false);

var webApi = builder.AddProject<Projects.CoreMonolith_WebApi>(ConnectionNameConstants.WebApiConnectionName)
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", coreWebApiEnv)
    .WithEnvironment(ConfigKeyConstants.JwtSecretKeyName, jwtSecret)
    .WithExternalHttpEndpoints()
    .WithReference(postgressDb)
    .WaitFor(postgres)
    .WaitFor(postgressDb)
    .WithReference(rabbitMq)
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(keycloak)
    .WaitFor(keycloak);
//-----------------------------------------------------------------------------------------//


//DownloadManager WebApp
var downloadManagerEnv = builder.AddParameter(ConfigKeyConstants.WebAppEnvKeyName, secret: false);

builder.AddProject<Projects.DownloadManager_WebApp>(ConnectionNameConstants.WebAppConnectionName)
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", downloadManagerEnv)
    .WithEnvironment(ConfigKeyConstants.JwtSecretKeyName, jwtSecret)
    .WithExternalHttpEndpoints()
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(keycloak)
    .WaitFor(keycloak)
    .WithReference(webApi)
    .WaitFor(webApi);

await builder.Build().RunAsync();
//-----------------------------------------------------------------------------------------//