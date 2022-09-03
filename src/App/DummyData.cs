using Scale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale
{
    internal class DummyData
    {
        public Scale.Models.Scale GetScale()
        {
            return new Scale.Models.Scale() { Name = "Balanza Precision", LastCalibration = DateTime.Now, Id = "Pre001", AdjustFactor = 4230, MaxWeight=10000};
        }

        public Recipe GetRecipe()
        {
            var r = new Recipe();
            r.Name = "Fragancia Suave";
            r.Items = new List<RecipeItem>();
            r.Items.Add(new RecipeItem() { Name = "Alcohol", MaxAllowedWeight = 5000, MinRequiredWeight = 3800, IdealWeight = 4250 });
            return r;
        }
    }
}
