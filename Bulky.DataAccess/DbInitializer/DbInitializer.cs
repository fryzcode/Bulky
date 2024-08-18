using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.DbInitializer;

public class DbInitializer : IDbInitializer
{
    
    private readonly UserManager<IdentityUser?> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _db;

    public DbInitializer(
        UserManager<IdentityUser?> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext db) {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
    }
    
    public void Initialize()
    {
        try
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            return;
        }
        
        if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
            
            _userManager.CreateAsync(new ApplicationUser() {
                UserName = "admin@bulky.com",
                Email = "admin@bulky.com",
                Name = "Admin",
                PhoneNumber = "994555555555",
                StreetAddress = "Yasamal",
                State = "Admin",
                PostalCode = "AZadmin",
                City = "Baku"
            }, "Admin123@").GetAwaiter().GetResult();
        
            ApplicationUser? user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@dotnetmastery.com");
            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
        }
        
        return;
    }
}