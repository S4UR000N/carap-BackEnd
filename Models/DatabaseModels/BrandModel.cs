using System.ComponentModel.DataAnnotations;

namespace api_storm.Models.DatabaseModels
{
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    
    }
}
