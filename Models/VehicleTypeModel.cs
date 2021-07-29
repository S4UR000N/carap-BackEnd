using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_storm.Models
{
    public class VehicleTypeModel
    {
        [Key]
        public int VehicleTypeId { get; set; }
        public string VehicleType { get; set; }
    }
}
