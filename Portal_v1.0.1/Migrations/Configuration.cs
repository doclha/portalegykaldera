namespace Portal_v1._0._1.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Portal_v1._0._1.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Portal_v1._0._1.Identity.IdentityDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Portal_v1._0._1.Identity.IdentityDataContext";
        }

        protected override void Seed(Portal_v1._0._1.Identity.IdentityDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "User" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "kaldera"))
            {
                var store = new UserStore<PortalUser>(context);
                var manager = new UserManager<PortalUser>(store);
                var user = new PortalUser { UserName = "kaldera", Name="Kaldera" , LastName="EGY", PhoneNumber="55555555" , Title="portaladmin" , Address="admin" , Email="kaldera@portal.com", IsCikis=DateTime.Now.ToLongDateString(),IseGiris= DateTime.Now, CiktiMi=false  };

                manager.Create(user, "kaldera123");
                manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
