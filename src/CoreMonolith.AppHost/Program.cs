using CoreMonolith.ServiceDefaults.Constants;

var builder = DistributedApplication.CreateBuilder(args);

//Core Postgres Db & PgAdmin
var postgresUser = builder.AddParameter(ConfigKeyConstants.DbUsernameKeyName, secret: false);
var postgresPassword = builder.AddParameter(ConfigKeyConstants.DbPasswordKeyName, secret: true);

var pgAdminHostPort = int.TryParse(builder.Configuration["AppConfig:CorePgAdminHostPort"], out int pgAdminPort) ? pgAdminPort : 8181;

var postgres = builder.AddPostgres(ResourceNameConstants.DbServerName, postgresUser, postgresPassword)
    .WithVolume($"{ResourceNameConstants.DbServerName}-volume", @"/var/lib/postgresql/data")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin(config =>
    {
        config
        .PublishAsContainer()
        .WithContainerName(ResourceNameConstants.DbServerAdminName)
        .WithVolume($"{ResourceNameConstants.DbServerAdminName}-volume", @"/var/lib/pgadmin")
        .WithHostPort(pgAdminHostPort)
        .WithExternalHttpEndpoints()
        .WithLifetime(ContainerLifetime.Persistent);
    });

var coreDb = postgres.AddDatabase(ConnectionNameConstants.CoreMonolithDbName);
var userServiceDb = postgres.AddDatabase(ConnectionNameConstants.UserServiceDbName);
var downloadServiceDb = postgres.AddDatabase(ConnectionNameConstants.DownloadServiceDbName);
//-----------------------------------------------------------------------------------------//


//Core RabbitMQ
var rabbitMqUser = builder.AddParameter(ConfigKeyConstants.RabbitMqUsernameKeyName, secret: false);
var rabbitMqPassword = builder.AddParameter(ConfigKeyConstants.RabbitMqPasswordKeyName, secret: true);

var rabbitMq = builder.AddRabbitMQ(ConnectionNameConstants.RabbitMqConnectionName, rabbitMqUser, rabbitMqPassword)
    .WithVolume($"{ConnectionNameConstants.RabbitMqConnectionName}-volume", @"/var/lib/rabbitmq")
    .WithManagementPlugin()
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
var keycloakUser = builder.AddParameter(ConfigKeyConstants.KeycloakUsernameKeyName, secret: false);
var keycloakPassword = builder.AddParameter(ConfigKeyConstants.KeycloakPasswordKeyName, secret: true);

var keycloakHostPort = int.TryParse(builder.Configuration["AppConfig:KeycloakHostPort"], out int keycloakPort) ? keycloakPort : 9191;

var keycloak = builder.AddKeycloak(ConnectionNameConstants.KeycloakConnectionName, keycloakHostPort, keycloakUser, keycloakPassword)
    .WithVolume($"{ConnectionNameConstants.KeycloakConnectionName}-volume", @"/opt/keycloak")
    .WithExternalHttpEndpoints()
    .WithLifetime(ContainerLifetime.Persistent);
//-----------------------------------------------------------------------------------------//


//Core Api
var coreApiEnv = builder.AddParameter(ConfigKeyConstants.ApiEnvKeyName, secret: false);

var api01 = builder.AddProject<Projects.CoreMonolith_Api>($"{ConnectionNameConstants.ApiConnectionName}-01", "core-api-01")
    .WithEnvironment(ConfigKeyConstants.AspCoreEnvVarKeyName, coreApiEnv)
    .WithReference(coreDb)
    .WithReference(userServiceDb)
    .WithReference(downloadServiceDb)
    .WaitFor(coreDb)
    .WaitFor(userServiceDb)
    .WaitFor(downloadServiceDb)
    .WithReference(rabbitMq)
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(keycloak)
    .WaitFor(keycloak);

var api02 = builder.AddProject<Projects.CoreMonolith_Api>($"{ConnectionNameConstants.ApiConnectionName}-02", "core-api-02")
    .WithEnvironment(ConfigKeyConstants.AspCoreEnvVarKeyName, coreApiEnv)
    .WithReference(coreDb)
    .WithReference(userServiceDb)
    .WithReference(downloadServiceDb)
    .WaitFor(coreDb)
    .WaitFor(userServiceDb)
    .WaitFor(downloadServiceDb)
    .WithReference(rabbitMq)
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(keycloak)
    .WaitFor(keycloak);

var api03 = builder.AddProject<Projects.CoreMonolith_Api>($"{ConnectionNameConstants.ApiConnectionName}-03", "core-api-03")
    .WithEnvironment(ConfigKeyConstants.AspCoreEnvVarKeyName, coreApiEnv)
    .WithReference(coreDb)
    .WithReference(userServiceDb)
    .WithReference(downloadServiceDb)
    .WaitFor(coreDb)
    .WaitFor(userServiceDb)
    .WaitFor(downloadServiceDb)
    .WithReference(rabbitMq)
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(keycloak)
    .WaitFor(keycloak);
//-----------------------------------------------------------------------------------------//


//Core ApiGateway
var clientSecret = builder.AddParameter(ConfigKeyConstants.WebAppClientSecret, secret: true);
var coreApiGatewayEnv = builder.AddParameter(ConfigKeyConstants.ApiGatewayEnvKeyName, secret: false);

var apiGateway = builder.AddProject<Projects.CoreMonolith_ApiGateway>(ConnectionNameConstants.ApiGatewayConnectionName)
    .WithEnvironment(ConfigKeyConstants.AspCoreEnvVarKeyName, coreApiGatewayEnv)
    .WithEnvironment(ConfigKeyConstants.WebAppClientSecret, clientSecret)
    .WithExternalHttpEndpoints()
    .WithReference(coreDb)
    .WithReference(userServiceDb)
    .WithReference(downloadServiceDb)
    .WaitFor(coreDb)
    .WaitFor(userServiceDb)
    .WaitFor(downloadServiceDb)
    .WithReference(rabbitMq)
    .WithReference(keycloak)
    .WaitFor(keycloak)
    .WithReference(api01)
    .WithReference(api02)
    .WithReference(api03)
    .WaitFor(api02)
    .WaitFor(api01)
    .WaitFor(api03);
//-----------------------------------------------------------------------------------------//


//DownloadManager WebApp
var downloadManagerEnv = builder.AddParameter(ConfigKeyConstants.WebAppEnvKeyName, secret: false);
var keycloakAuthority = builder.AddParameter(ConfigKeyConstants.KeycloakAuthorityKeyName, secret: false);

builder.AddProject<Projects.DownloadManager_WebApp>(ConnectionNameConstants.WebAppConnectionName)
    .WithEnvironment(ConfigKeyConstants.AspCoreEnvVarKeyName, downloadManagerEnv)
    .WithEnvironment(ConfigKeyConstants.WebAppClientSecret, clientSecret)
    .WithEnvironment(ConfigKeyConstants.KeycloakAuthorityKeyName, keycloakAuthority)
    .WithExternalHttpEndpoints()
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(keycloak)
    .WaitFor(keycloak)
    .WithReference(apiGateway)
    .WaitFor(apiGateway)
    .WaitFor(keycloak);
//-----------------------------------------------------------------------------------------//


await builder.Build().RunAsync(default);