using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api_storm.Models.DatabaseModels;

namespace api_storm.Data
{
    public class api_stormContext : DbContext
    {
        public api_stormContext (DbContextOptions<api_stormContext> options)
            : base(options)
        {
        }

        public DbSet<api_storm.Models.DatabaseModels.BrandModel> BrandModel { get; set; }

        public DbSet<api_storm.Models.DatabaseModels.VehicleTypeModel> VehicleTypeModel { get; set; }

        public DbSet<api_storm.Models.DatabaseModels.VehicleModel> VehicleModel { get; set; }
    }
}
