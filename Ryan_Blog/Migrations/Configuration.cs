namespace Ryan_Blog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Ryan_Blog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Ryan_Blog.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "your email address"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "chapman.ryansadler@gmail.com",
                    Email = "chapman.ryansadler@gmail.com",
                    FirstName = "Ryan",
                    LastName = "Chapman",
                    DisplayName = "Ryan Chapman",
                }, "Chappy24!");
            }

            var userId_Admin = userManager.FindByEmail("chapman.ryansadler@gmail.com").Id;
            userManager.AddToRole(userId_Admin, "Admin");

            if (!context.Users.Any(u => u.Email == "your email address"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "chapman.ryansadler.M@gmail.com",
                    Email = "chapman.ryansadler.M@gmail.com",
                    FirstName = "Ryan",
                    LastName = "Chapman",
                    DisplayName = "The Law",
                }, "Chappy24!");
            }

            var userId_TheLaw = userManager.FindByEmail("chapman.ryansadler.M@gmail.com").Id;
            userManager.AddToRole(userId_TheLaw, "Moderator");

            if (!context.Users.Any(u => u.Email == "your email address"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "rmanglani@coderfoundry.com",
                    Email = "rmanglani@coderfoundry.com",
                    FirstName = "Ria",
                    LastName = "Manglani",
                    DisplayName = "Ria Manglani",
                }, "Abcdefg-1");
            }

            var userId_Ria = userManager.FindByEmail("rmanglani@coderfoundry.com").Id;
            userManager.AddToRole(userId_Ria, "Moderator");

            if (!context.Users.Any(u => u.Email == "your email address"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "araynor@coderfoundry.com",
                    Email = "araynor@coderfoundry.com",
                    FirstName = "Antonio",
                    LastName = "Raynor",
                    DisplayName = "Antonio Raynor",
                }, "Abcdefg-1");
            }

            var userId_Antonio = userManager.FindByEmail("araynor@coderfoundry.com").Id;
            userManager.AddToRole(userId_Antonio, "Moderator");

            if (!context.Users.Any(u => u.Email == "your email address"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "moderator@coderfoundry.com",
                    Email = "moderator@coderfoundry.com",
                    FirstName = "Coder",
                    LastName = "Foundry",
                    DisplayName = "Coder Foundry",
                }, "Password-1");
            }

            var userId_Coder = userManager.FindByEmail("moderator@coderfoundry.com").Id;
            userManager.AddToRole(userId_Coder, "Moderator");

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
