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

        public DbSet<BrandModel> BrandModel { get; set; }

        public DbSet<VehicleTypeModel> VehicleTypeModel { get; set; }

        public DbSet<VehicleModel> VehicleModel { get; set; }
    }
}
