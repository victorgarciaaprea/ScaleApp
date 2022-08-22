using Avalonia.Threading;
using Scale.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Scale.ViewModels
{
    public class WeightViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public double Weight { get; set; }
        bool canContinue;
        int currentWeight;
        string weightBgColor;
        string weightFgColor;
        RecipeItem recipeItem;
        Recipe recipe;
        int itemCount = 0;

        public WeightViewModel()
        {
            this.recipe = Program.GetSampleRecipe();
            SetupParams();
        }

        public WeightViewModel(Recipe recipe)
        {
            this.recipe = recipe;
            if (recipe.Items.Count == 0)
            {
                throw new ArgumentException("Recipe must contain at least one item");
            }

            SetupParams();
        }

        void SetupParams()
        {
            this.currentWeight = 0;
            this.weightBgColor = AppSettings.WeightBgColor;
            this.weightFgColor = AppSettings.WeightFgColor;
            this.canContinue = false;
            this.RecipeItem = this.recipe.Items[itemCount++];
        }

        public int CurrentWeight
        {
            get { return currentWeight; }
            set
            {
                if (value != currentWeight)
                {
                    currentWeight = value;
                    Dispatcher.UIThread.InvokeAsync(() => NotifyPropertyChanged(nameof(CurrentWeight)));
                }
            }
        }

        public void GoNext()
        {
            // if we just handled the last recie item then we need to a "finished" recipe view
            if (itemCount == recipe.Items.Count)
            {
                return;
            }

            // keep moving to the next recipe item
            RecipeItem = recipe.Items[itemCount++];
        }

        public RecipeItem RecipeItem
        {
            get { return recipeItem; }
            set
            {
                if (value != recipeItem)
                {
                    recipeItem = value;
                    Dispatcher.UIThread.InvokeAsync(() => NotifyPropertyChanged(nameof(RecipeItem)));
                }
            }
        }

        public string ScaleName
        {
            get
            {
                return new DummyData().GetScale().Name;
            }
        }

        public string ScaleId
        {
            get
            {
                return new DummyData().GetScale().Id;
            }
        }

        public string WeightFgColor
        {
            get { return weightFgColor; }
            set
            {
                if (value != weightFgColor)
                {
                    weightFgColor = value;
                    Dispatcher.UIThread.InvokeAsync(() => NotifyPropertyChanged(nameof(WeightFgColor)));
                }
            }
        }
        public string WeightBgColor
        {
            get { return weightBgColor; }
            set
            {
                if (value != weightBgColor)
                {
                    weightBgColor = value;
                    Dispatcher.UIThread.InvokeAsync(() => NotifyPropertyChanged(nameof(WeightBgColor)));
                }
            }
        }

        public bool CanContinue
        {
            get { return canContinue; }
            set
            {
                if (value != canContinue)
                {
                    canContinue = value;
                    Dispatcher.UIThread.InvokeAsync(() => NotifyPropertyChanged(nameof(CanContinue)));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
