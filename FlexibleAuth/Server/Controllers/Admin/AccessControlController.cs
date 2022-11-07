using FlexibleAuth.Server.Models;
using FlexibleAuth.Shared;
using FlexibleAuth.Shared.Authorization;
using InfiniteEnumFlags;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexibleAuth.Server.Controllers.Admin;

[ApiController]
[Route("api/Admin/[controller]")]
public class AccessControlController : ControllerBase
{
    private readonly RoleManager<Role> _roleManager;

    public AccessControlController(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpGet]
    [Authorize(nameof(Permission.ViewAccessControl))]
    public async Task<ActionResult<AccessControlVm>> GetConfiguration()
    {
        var roles = await _roleManager.Roles
            .ToListAsync();

        var roleDtos = roles
            .Select(r => new RoleDto(r.Id, r.Name, r.Permission.ToBase64Key()))
            .OrderBy(r => r.Name)
            .ToList();

        return new AccessControlVm(roleDtos);
    }

    [HttpPut]
    [Authorize(nameof(Permission.ConfigureAccessControl))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateConfiguration(RoleDto updatedRole)
    {
        var role = await _roleManager.FindByIdAsync(updatedRole.Id);

        if (role != null)
        {
            role.Permission = Flag<Permission>.FromBase64(updatedRole.Permissions);

            await _roleManager.UpdateAsync(role);
        }

        return NoContent();
    }
}