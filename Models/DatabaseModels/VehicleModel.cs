using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_storm.Models.DatabaseModels
{
    public class VehicleModel
    {
        [Key]
        public int VehicleId { get; set; }
        public int BrandNameId { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleName { get; set; }
    }
}
