using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace Bloggie.web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var userRoleId ="3565de8c-761b-4a18-8e92-dee8f8c22581";
            var adminRoleId ="14df2fbb-af4c-4560-a2e4-5894926dc23e";
            var superAdminRoleId = "9e8c688c-4042-4c4c-abdc-40478a6cc96d";
            //seed roles(user,admin,super admin)
            //seed identity Roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="User",
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId

                },
                  new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="Admin",
                    Id=adminRoleId ,
                    ConcurrencyStamp=adminRoleId

                },
                    new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId

                },

            };
           
            builder.Entity<IdentityRole>().HasData(roles);

            //Seed Identity user
            //seed superadmin user
            var SuperAdminId = "36f3c068-4154-4d5b-98be-26af8d99cfb8";
            var superAdminUser = new IdentityUser
            {
                UserName = "Superadmin@bloggoe.com",
                Email = "Superadmin@bloggoe.com",
                NormalizedEmail = "Superadmin@bloggoe.com".ToUpper(),
                NormalizedUserName= "Superadmin@bloggoe.com".ToUpper(),
                Id = SuperAdminId,
                
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser,"Superadmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            //Add all roles to SuperAdminUsers

            var SuperAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId=userRoleId  ,
                    UserId= SuperAdminId
                },
                 new IdentityUserRole<string>
                {
                    RoleId=adminRoleId,
                    UserId= SuperAdminId
                },
                  new IdentityUserRole<string>
                {
                    RoleId=superAdminRoleId,
                    UserId= SuperAdminId
                },
                                 

            };
            builder.Entity<IdentityUserRole<string>>().HasData(SuperAdminRoles);


        }


    }
}
