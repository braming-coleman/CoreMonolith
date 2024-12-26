namespace CoreMonolith.ServiceDefaults.Constants;

public static class ConnectionNameConstants
{
    public static string UserServiceDbName => "user-service-db";
    public static string RedisConnectionName => "core-monolith-redis";
    public static string KeycloakConnectionName => "core-monolith-keycloak";
    public static string RabbitMqConnectionName => "core-monolith-mq";
    public static string ApiConnectionName => "core-monolith-api";
    public static string ApiGatewayConnectionName => "core-monolith-api-gateway";
    public static string WebAppConnectionName => "download-manager-webapp";
}
