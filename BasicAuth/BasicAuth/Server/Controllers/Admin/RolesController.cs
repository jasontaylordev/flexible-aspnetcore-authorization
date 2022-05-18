using BasicAuth.Server.Models;
using BasicAuth.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicAuth.Server.Controllers.Admin;

[Authorize(Roles = "Administrators")]
[ApiController]
[Route("api/Admin/[controller]")]
public class RolesController : ControllerBase
{
    private readonly RoleManager<Role> _roleManager;

    public RolesController(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    // GET: api/Admin/Roles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
    {
        var roles = await _roleManager.Roles
            .OrderBy(r => r.Name)
            .ToListAsync();

        return roles
            .Select(r => new RoleDto(r.Id, r.Name))
            .ToList();
    }
}
