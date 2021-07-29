using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_storm.Models
{
    public class VehicleModel
    {
        [Key]
        public int VehicleId { get; set; }
        public string BrandName { get; set; }
        public string VehicleType { get; set; }
        public string VehicleName { get; set; }
    }
}
