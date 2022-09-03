using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale.Models
{
    public class Recipe
    {
        public WeightSettings WeightSettings { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Recipe()
        {
            Items = new List<RecipeItem>();
        }

        public List<RecipeItem> Items { get; set; }

    }
}
