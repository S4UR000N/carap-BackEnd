using System.ComponentModel.DataAnnotations;

namespace api_storm.Models.DatabaseModels
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
        public int BrandId { get; set; }
        public virtual BrandModel Brand { get; set; }

        public int VehicleTypeId { get; set; }
        public virtual VehicleTypeModel VehicleType { get; set; }
                
        public string ModelName { get; set; }
    }
}
