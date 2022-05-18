using BasicAuth.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BasicAuth.Server.Data;

public class DbInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    private const string AdministratorsRole = "Administrators";
    private const string AccountsRole = "Accounts";
    private const string OperationsRole = "Operations";

    private const string DefaultPassword = "Password123!";

    public DbInitializer(
        ApplicationDbContext context,
        UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task RunAsync()
    {
        _context.Database.Migrate();

        // Create roles
        await _roleManager.CreateAsync(new Role { Name = AdministratorsRole, NormalizedName = AdministratorsRole.ToUpper() });
        await _roleManager.CreateAsync(new Role { Name = AccountsRole, NormalizedName = AccountsRole.ToUpper() });
        await _roleManager.CreateAsync(new Role { Name = OperationsRole, NormalizedName = OperationsRole.ToUpper() });

        // Create default admin user
        var adminUserName = "admin@localhost";
        var adminUser = new User { UserName = adminUserName, Email = adminUserName };
        await _userManager.CreateAsync(adminUser, DefaultPassword);

        adminUser = await _userManager.FindByNameAsync(adminUserName);
        await _userManager.AddToRoleAsync(adminUser, AdministratorsRole);

        // Create default auditor user
        var auditorUserName = "auditor@localhost";
        var auditorUser = new User { UserName = auditorUserName, Email = auditorUserName };
        await _userManager.CreateAsync(auditorUser, DefaultPassword);

        await _context.SaveChangesAsync();
    }
}
