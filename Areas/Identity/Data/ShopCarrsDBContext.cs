using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopCarrs.Areas.Identity.Data;

namespace ShopCarrs.Data;

public class ShopCarrsDBContext : IdentityDbContext<ApplicationUser>
{
    public ShopCarrsDBContext(DbContextOptions<ShopCarrsDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        //create roles
        // Create a variable to store the IDs of the roles
        string adminRoleId = Guid.NewGuid().ToString();
        string customerRoleId = Guid.NewGuid().ToString();
        // Create the roles and add them to the database
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = adminRoleId,
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR".ToUpper()
        });

        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = customerRoleId,
            Name = "Customer",
            NormalizedName = "CUSTOMER".ToUpper()
        });
        // Create a hasher to hash the password before seeding the user to the db
        var hasher = new PasswordHasher<IdentityUser>();

        // Create the administrator user and add it to the database
        string adminUserId = Guid.NewGuid().ToString();
        builder.Entity<ApplicationUser>().HasData(new ApplicationUser
        {
            Id = adminUserId,
            UserName = "admin@gmail.com",
            FirstName = "admin",
            LastName = "admin",
            PhoneNumber = "1234567890",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            NormalizedUserName = "ADMIN@GMAIL.COM",
            PasswordHash = hasher.HashPassword(null, "123456")
        });
        // Create the customer user and add it to the database
        string customerUserId = Guid.NewGuid().ToString();
        builder.Entity<ApplicationUser>().HasData(new ApplicationUser
        {
            Id = customerUserId,
            UserName = "customer@gmail.com",
            FirstName = "customer",
            LastName = "customer",
            PhoneNumber = "1234567890",
            Email = "customer@gmail.com",
            NormalizedEmail = "CUSTOMER@GMAIL.COM",
            NormalizedUserName = "CUSTOMER@GMAIL.COM",
            PasswordHash = hasher.HashPassword(null, "123456")
        });
        // Add the administrator user to the administrator role
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = adminRoleId,
            UserId = adminUserId
        });

        // Add the customer user to the customer role
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = customerRoleId,
            UserId = customerUserId
        });



    }
}
