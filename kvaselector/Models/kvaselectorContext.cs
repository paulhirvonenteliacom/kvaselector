using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace kvaselector.Models
{
    public class kvaselectorContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public kvaselectorContext() : base("name=kvaselectorContext")
        {
        }

        public DbSet<kvaselector.Models.Diagnosis> Diagnosis { get; set; }
        public DbSet<kvaselector.Models.Treatment> Treatments { get; set; }
        public DbSet<kvaselector.Models.TreatmentType> TreatmentTypes { get; set; }
        public DbSet<kvaselector.Models.Service> Services { get; set; }
        public DbSet<kvaselector.Models.ServiceType> ServiceTypes { get; set; }
        public DbSet<kvaselector.Models.FavoriteService> FavoriteServices { get; set; }
        public DbSet<kvaselector.Models.FavoriteName> FavoriteNames { get; set; }
    }
}
