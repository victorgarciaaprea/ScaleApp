using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale.Models
{
    public class RecipeItem
    {
        public WeightSettings WeightSettings { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; } 
        public string Id { get; set; }
        public int MinRequiredWeight { get; set; }
        public int MaxAllowedWeight { get; set; }
        public int IdealWeight { get; set; }
    }
}
