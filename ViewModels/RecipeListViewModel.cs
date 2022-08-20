using Scale.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale.ViewModels
{
    public  class RecipeListViewModel : ViewModelBase
    {
        public RecipeListViewModel()
        {
            Recipes = new ObservableCollection<Recipe>() { new Recipe() { Name = "Fragancia Suave" }, new Recipe() { Name = "Fragancia Verde" } };
        }
        public ObservableCollection<Recipe> Recipes { get; }

    }
}
