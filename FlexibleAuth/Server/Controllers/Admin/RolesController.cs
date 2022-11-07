using FlexibleAuth.Server.Models;
using FlexibleAuth.Shared;
using FlexibleAuth.Shared.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexibleAuth.Server.Controllers.Admin;

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
    [Authorize(Permissions.ViewRoles)]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
    {
        var roles = await _roleManager.Roles
            .OrderBy(r => r.Name)
            .ToListAsync();

        return roles
            .Select(r => new RoleDto(r.Id, r.Name, r.Permissions))
            .ToList();
    }

    // POST: api/Admin/Roles
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Permissions.ManageRoles)]
    public async Task<ActionResult<RoleDto>> PostRole(RoleDto newRole)
    {
        var role = new Role { Name = newRole.Name };

        await _roleManager.CreateAsync(role);

        return new RoleDto(role.Id, role.Name, role.Permissions);
    }

    // PUT: api/Admin/Roles/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Permissions.ManageRoles)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutRole(string id, RoleDto updatedRole)
    {
        if (id != updatedRole.Id)
        {
            return BadRequest();
        }

        var role = await _roleManager.FindByIdAsync(id);

        role.Name = updatedRole.Name;

        await _roleManager.UpdateAsync(role);

        if (role == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Admin/Roles/5
    [HttpDelete("{id}")]
    [Authorize(Permissions.ManageRoles)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        await _roleManager.DeleteAsync(role);

        return NoContent();
    }
}
