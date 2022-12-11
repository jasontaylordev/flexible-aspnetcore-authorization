using System.Reflection;
using InfiniteEnumFlags;

namespace FlexibleAuth.Shared.Authorization;

public static class PermissionsProvider
{
    public static Dictionary<string,Flag<Permission>> GetAll()
    {
        return Permission.GetKeyValues();
    }
}
