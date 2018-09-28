using Microsoft.AspNet.Identity.EntityFramework;
using Portal_v1._0._1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Portal_v1._0._1.Identity
{
    public class IdentityDataContext : IdentityDbContext<PortalUser>
    {
        public IdentityDataContext() : base("dbConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<IdentityDataContext, Portal_v1._0._1.Migrations.Configuration>());
        }

        public DbSet<IzinModel> Izinler { get; set; }
        public DbSet<RaporModel> Raporlar { get; set; }
        public DbSet<MasrafUrun> MasrafUrunler { get; set; }
        public DbSet<ResmiTatiller> ResmiTatiller { get; set; }
        public DbSet<MasrafSepet> MasrafSepetler { get; set; }
        public DbSet<CVModel> CVler { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}