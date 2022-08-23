using Avalonia.Threading;
using Scale.Models;
using Scale.Scales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        string bigButtonText;
        RecipeItem recipeItem;
        Recipe recipe;
        bool showWeight;
        int itemCount = 0;
        int previousWeight = 0;
        private MainWindowViewModel mainWindowViewModel;
        IScale scale;

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

        public WeightViewModel(Recipe recipe, MainWindowViewModel mainWindowViewModel) : this(recipe)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        async void SetupParams()
        {
            this.bigButtonText = "Siguiente";
            this.currentWeight = 0;
            this.showWeight = true;
            this.weightBgColor = AppSettings.WeightBgColor;
            this.weightFgColor = AppSettings.WeightFgColor;
            this.canContinue = false;
            this.RecipeItem = this.recipe.Items[itemCount++];

            // create IScale from data in recipe (each recipe should tell us what scale to use)
            byte dout = 5;
            byte pd_sck = 6;
            int referenceUnit = 387;

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                this.scale = new Hx711BasedScale(dout, pd_sck, referenceUnit);
            }
            else
            {
                this.scale = new FakeScale();
            }

            this.scale.ValueChanged += Scale_ValueChanged;
            await this.scale.Start();
        }

        private void Scale_ValueChanged(object? sender, EventArgs e)
        {
            var val = sender as IScale;
            CurrentWeight = val.GetValue();
            UpdateStatus();
        }

        public int CurrentWeight
        {
            get { return currentWeight - previousWeight; }
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
            // if we just handled the last recipe item then we need to a "finished" recipe view
            if (itemCount == recipe.Items.Count)
            {
                this.scale.ValueChanged -= Scale_ValueChanged;
                ShowWeight = false;
                BigButtonText = "Finalizar";
                //mainWindowViewModel.NavigateTo(new LoginViewModel());
                return;
            }  

            // take into account previous recipe item weight
            //previousWeight += currentWeight;
            scale.Tare();
            CurrentWeight = 0;
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
        public bool ShowWeight
        {
            get { return showWeight; }
            set
            {
                if (value != showWeight)
                {
                    showWeight = value;
                    Dispatcher.UIThread.InvokeAsync(() => NotifyPropertyChanged(nameof(ShowWeight)));
                }
            }
        }
        public string BigButtonText
        {
            get { return bigButtonText; }
            set
            {
                if (value != bigButtonText)
                {
                    bigButtonText = value;
                    Dispatcher.UIThread.InvokeAsync(() => NotifyPropertyChanged(nameof(BigButtonText)));
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

        void UpdateStatus()
        {
            if (CurrentWeight < RecipeItem.MinRequiredWeight)
            {
                WeightFgColor = AppSettings.WeightFgColor;
                WeightBgColor = AppSettings.WeightBgColor;
                CanContinue = false;
            }
            else if (CurrentWeight >= RecipeItem.MinRequiredWeight && CurrentWeight <= RecipeItem.MaxAllowedWeight)
            {
                WeightFgColor = AppSettings.IdealWeightFgColor;
                WeightBgColor = AppSettings.IdealWeightBgColor;
                CanContinue = true;
            }
            else
            {
                if (!IsWeightOverloadAllowed())
                {
                    WeightFgColor = AppSettings.ErrorWeightFgColor;
                    WeightBgColor = AppSettings.ErrorWeightBgColor;
                    CanContinue = false;
                }
            }
        }

        // weight overload support can be defined at the recipe item level or the recipe level itself
        // recipe item takes precedence over recipe
        // if none is specified, per app default value is used
        bool IsWeightOverloadAllowed()
        {
            var allowOverloadSetting = recipeItem.WeightSettings ?? recipe.WeightSettings;
            return allowOverloadSetting?.AllowWeightOverload ?? AppSettings.AllowWeightOverload;
        }

    }
}
