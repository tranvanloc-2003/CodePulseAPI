using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//tắt cảnh báo khi thêm vào sql
        //{
        //    optionsBuilder.ConfigureWarnings(warnings =>
        //        warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "685c72e8-fe60-4f64-90bd-a10ff36c94b3";
            var writeRoleId = "c2ade0a4-1cf0-41cc-a482-fe3256f5162d";

            //tao nguoi doc va viet vai tro
            var roles = new List<IdentityRole>
            {

                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writeRoleId,
                    Name = "Write",
                    NormalizedName = "Write".ToUpper(),
                    ConcurrencyStamp = writeRoleId
                }
            };
            // Gieo vai tro
            builder.Entity<IdentityRole>().HasData(roles);

            //tao mot nguoi dung quan tri vien
            var adminUserId = "aa4c6ff8-71a8-4bb4-8719-480c45f16bef";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@gmail.com",
                Email = "admin@mail.com",
                NormalizedEmail = "admin@mail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper(),
            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "admin");

            builder.Entity<IdentityUser>().HasData(admin);

            //cap vai tro cho quan tri vien
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId,
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writeRoleId,
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);


        }
    }
}
