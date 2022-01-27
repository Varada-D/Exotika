
using ExotikaTrial2.Data;
using ExotikaTrial2.Models;
using ExotikaTrial2.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExotikaTrial2.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ExotikaTrial2Context _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ExotikaTrial2Context db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }
        public void Initialize()
        {
            // Apply migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex)
            {

            }



            // Create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Tourist)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Vendor)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Resort)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_HandicraftDealer)).GetAwaiter().GetResult();

                // If roles are not created, then create admin user as well
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@exotika.com",
                    Email = "admin@exotika.com",
                    Name = "Exotika Admin",
                    PhoneNumber = "9856748930",
                    StreetAddress = "123 Test Street",
                    State = "Admin Test State",
                    PostalCode = "432563",
                    City = "AdminCity",
                    Role = SD.Role_Admin,
                    CreateDate = DateTime.Now
                },"Admin@123").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@exotika.com");

                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();


                var adminUser = new Admin()
                {
                    AdminId = user.Id,
                    Name = user.Name,
                    StreetAddress = user.StreetAddress,
                    City = user.City,
                    State = user.State,
                    PostalCode = user.PostalCode,
                    PhoneNumber = user.PhoneNumber,
                    emailAddr = user.Email,
                    createDate = user.CreateDate
                };
                _db.Admins.Add(adminUser);
                _db.SaveChanges();
            }
            return;
        }
    }
}
