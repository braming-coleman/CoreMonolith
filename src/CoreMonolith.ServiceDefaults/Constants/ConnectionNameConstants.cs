namespace CoreMonolith.ServiceDefaults.Constants;

public static class ConnectionNameConstants
{
    public static string DbConnStringName => "core-monolith-db";
    public static string RedisConnectionName => "core-monolith-redis";
    public static string KeycloakConnectionName => "core-monolith-keycloak";
    public static string RabbitMqConnectionName => "core-monolith-mq";
    public static string WebApiConnectionName => "core-monolith-webapi";
    public static string WebAppConnectionName => "download-manager-webapp";
}
