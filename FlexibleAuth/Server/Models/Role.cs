using FlexibleAuth.Shared.Authorization;
using InfiniteEnumFlags;
using Microsoft.AspNetCore.Identity;

namespace FlexibleAuth.Server.Models;

public class Role : IdentityRole
{
    public Flag<Permission> Permission { get; set; } = new();
}
