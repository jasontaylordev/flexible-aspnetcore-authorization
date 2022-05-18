using FlexibleAuth.Server.Models;
using FlexibleAuth.Shared;
using FlexibleAuth.Shared.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexibleAuth.Server.Controllers.Admin
{
    [ApiController]
    [Route("api/Admin/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/Admin/Users
        [HttpGet]
        [Authorize(Permissions.ViewUsers | Permissions.ManageUsers)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            return await _userManager.Users
                .OrderBy(r => r.UserName)
                .Select(u => new UserDto(u.Id, u.UserName, u.Email))
                .ToListAsync();
        }

        // GET: api/Admin/Users/5
        [HttpGet("{id}")]
        [Authorize(Permissions.ViewUsers)]
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var dto = new UserDto(user.Id, user.UserName, user.Email);

            var roles = await _userManager.GetRolesAsync(user);

            dto.Roles.AddRange(roles);

            return dto;
        }

        // PUT: api/Admin/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Permissions.ManageUsers)]
        public async Task<IActionResult> PutUser(string id, UserDto updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;

            await _userManager.UpdateAsync(user);

            var currentRoles = await _userManager.GetRolesAsync(user);
            var addedRoles = updatedUser.Roles.Except(currentRoles);
            var removedRoles = currentRoles.Except(updatedUser.Roles);

            if (addedRoles.Any())
            {
                await _userManager.AddToRolesAsync(user, addedRoles);
            }

            if (removedRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, removedRoles);
            }

            return NoContent();
        }
    }
}
