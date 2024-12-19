namespace CoreMonolith.ServiceDefaults.Constants;

public static class ConfigKeyConstants
{
    public static string WebApiEnvKeyName => "core-monolith-webapi-env";
    public static string JwtSecretKeyName => "core-monolith-webapi-jwt-secret";

    public static string DbUsernameKeyName => "core-monolith-db-username";
    public static string DbPasswordKeyName => "core-monolith-db-password";

    public static string RabbitMqUsernameKeyName => "core-monolith-mq-username";
    public static string RabbitMqPasswordKeyName => "core-monolith-mq-password";

    public static string KeycloakUsernameKeyName => "core-monolith-keycloak-username";
    public static string KeycloakPasswordKeyName => "core-monolith-keycloak-password";

    public static string WebAppEnvKeyName => "download-manager-webapp-env";
}
