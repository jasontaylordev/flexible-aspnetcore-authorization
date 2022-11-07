using FlexibleAuth.Shared.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FlexibleAuth.Server.Models;

public class Role : IdentityRole
{
    public Permissions Permissions { get; set; }
}
