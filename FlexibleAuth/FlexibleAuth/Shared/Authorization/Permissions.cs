namespace FlexibleAuth.Shared.Authorization;

[Flags]
public enum Permissions
{
    None = 0,
    ViewRoles = 1,
    ManageRoles = 2,
    ViewUsers = 4,
    ManageUsers = 8,
    ConfigureAccessControl = 16,
    Counter = 32,
    Forecast = 64,
    All = ~None
}