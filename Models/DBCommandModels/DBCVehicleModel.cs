using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_storm.Models.DBCommandModels
{
    public class DBCVehicleModel
    {
        public int BrandId { get; set; }
        public int VehicleTypeId { get; set; }
        public string ModelName { get; set; }
    }
}
