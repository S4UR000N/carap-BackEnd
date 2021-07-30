using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_storm.Models
{
    public class BrandModel
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    
    }
}
