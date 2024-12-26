namespace Modules.UserService.Infrastructure.Database.Configs;

internal static class AccessSeedDataConstants
{
    public static Guid PermissionId_UserRead => Guid.Parse("0193eb73-d636-750a-a839-5571f30fd6b2");
    public static Guid PermissionId_UserWrite => Guid.Parse("0193eb73-d636-7aed-bbbc-963672568d66");
    public static Guid PermissionId_PermissionGroupRead => Guid.Parse("0193eb73-d636-770f-9d4d-6f2c6d9ccac3");
    public static Guid PermissionId_PermissionGroupWrite => Guid.Parse("0193eb73-d636-79e6-b669-87236dbbaa96");
    public static Guid PermissionId_PermissionRead => Guid.Parse("0193eb73-d636-72c1-b3fb-52c82f3593ac");
    public static Guid PermissionId_PermissionWrite => Guid.Parse("0193eb73-d636-7150-a2bc-13bde0f65734");
    public static Guid PermissionId_WeatherRead => Guid.Parse("0193ed22-aa79-7cb1-9b74-11345e299d89");
    public static Guid PermissionId_ApiGatewayAccess => Guid.Parse("0193f4d3-e613-72e7-8917-a9849ec17bc6");

    public static Guid PermissionGroupId_Admin => Guid.Parse("0193ec1f-35c9-723c-a203-67c5e4e0eb75");
    public static Guid PermissionGroupId_User => Guid.Parse("0193ec1f-35c9-747a-91f1-5601ca02c36f");
}
