using InfiniteEnumFlags;

namespace FlexibleAuth.Shared.Authorization;

// [Flags]
// public enum Permissions2
// {
//     None = 0,
//     ViewRoles = 1,
//     ManageRoles = 2,
//     ViewUsers = 4,
//     ManageUsers = 8,
//     ConfigureAccessControl = 16,
//     Counter = 32,
//     Forecast = 64,
//     ViewAccessControl = 128,
//     All = ~None
// }

public class Permission : InfiniteEnum<Permission>
{
    public static readonly Flag<Permission> None = new(-1);
    public static readonly Flag<Permission> ViewRoles = new(0);
    public static readonly Flag<Permission> ManageRoles = new(1);
    public static readonly Flag<Permission> ViewUsers = new(2);
    public static readonly Flag<Permission> ManageUsers = new(3);
    public static readonly Flag<Permission> ConfigureAccessControl = new(4);
    public static readonly Flag<Permission> Counter = new(5);
    public static readonly Flag<Permission> Forecast = new(6);
    public static readonly Flag<Permission> ViewAccessControl = new(7);

    // We can support up to 2,147,483,647 items

}