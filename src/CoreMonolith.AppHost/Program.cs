using CoreMonolith.ServiceDefaults.Constants;

var builder = DistributedApplication.CreateBuilder(args);

//Core Postgres Db & PgAdmin
var postgresUser = builder.AddParameter(ConfigKeyConstants.DbUsernameKeyName, secret: false);
var postgresPassword = builder.AddParameter(ConfigKeyConstants.DbPasswordKeyName, secret: true);

var pgAdminHostPort = int.TryParse(builder.Configuration["AppConfig:CorePgAdminHostPort"], out int pgAdminPort) ? pgAdminPort : 8181;

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
        .WithHostPort(pgAdminHostPort)
        .WithExternalHttpEndpoints()
        .WithLifetime(ContainerLifetime.Persistent);
    });

var postgressDb = postgres.AddDatabase(ConnectionNameConstants.DbConnStringName);
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

var api = builder.AddProject<Projects.CoreMonolith_Api>(ConnectionNameConstants.ApiConnectionName)
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", coreApiEnv)
    .WithReference(postgressDb)
    .WaitFor(postgres)
    .WaitFor(postgressDb)
    .WithReference(rabbitMq)
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(keycloak)
    .WaitFor(keycloak);
//-----------------------------------------------------------------------------------------//


//Core ApiGateway
var coreApiGatewayEnv = builder.AddParameter(ConfigKeyConstants.ApiGatewayEnvKeyName, secret: false);

var apiGateway = builder.AddProject<Projects.CoreMonolith_ApiGateway>(ConnectionNameConstants.ApiGatewayConnectionName)
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", coreApiGatewayEnv)
    .WithExternalHttpEndpoints()
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(keycloak)
    .WaitFor(keycloak)
    .WithReference(api)
    .WaitFor(api);
//-----------------------------------------------------------------------------------------//


//DownloadManager WebApp
var clientSecret = builder.AddParameter(ConfigKeyConstants.WebAppClientSecret, secret: true);
var downloadManagerEnv = builder.AddParameter(ConfigKeyConstants.WebAppEnvKeyName, secret: false);

builder.AddProject<Projects.DownloadManager_WebApp>(ConnectionNameConstants.WebAppConnectionName)
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", downloadManagerEnv)
    .WithEnvironment(ConfigKeyConstants.WebAppClientSecret, clientSecret)
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