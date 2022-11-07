using System.Reflection;
using InfiniteEnumFlags;

namespace FlexibleAuth.Shared.Authorization;

public static class PermissionsProvider
{
    public static Dictionary<string,Flag<Permission>> GetAll()
    {
        return typeof(Permission)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(Flag<Permission>))
            .ToDictionary(f => f.Name, f => (Flag<Permission>) f.GetValue(null)!);
    }
}
