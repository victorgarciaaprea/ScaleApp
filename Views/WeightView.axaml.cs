using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HX711DotNet;
using Scale.ViewModels;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

namespace Scale.Views
{
    public partial class WeightView : UserControl
    {
        public WeightView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        //RecipeItem item;
        public async void OnClickGoNext(object? sender, RoutedEventArgs args)
        {
            var recipe = new DummyData().GetRecipe();
        }

        public async void OnClickChangeWeight(object? sender, RoutedEventArgs args)
        {
            //var dc = DataContext as WeightViewModel;
            //var random = new Random();

            //dc.CurrentWeight = random.NextDouble();

            var recipe = new DummyData().GetRecipe();

            

            await ReadWeightFromScale();
            return;
            //var dc = DataContext as MainWindowViewModel;
            //(sender as Button)!.Content = dc.Password;
        }

        private byte dout = 5;
        private byte pd_sck = 6;
        int referenceUnit = 387;

        public async Task ReadWeightFromScale()
        {
            var dc = DataContext as WeightViewModel;
            if (dc == null)
            {
                throw new ArgumentException("DataConext is null");
            }


            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Pi.Init<BootstrapWiringPi>();
                var hx = new HX711(dout, pd_sck);
                hx.SetReferenceUnit(referenceUnit);

                hx.Reset();
                hx.Tare();

                while (true)
                {
                    var val = hx.GetWeight(5);
                    hx.PowerDown();
                    hx.PowerUp();
                    Thread.Sleep(100);

                    if (val >= -2 && val <= 2)
                        val = 0;

                    dc.CurrentWeight = val;
                    UpdateState(dc);
                    await Task.Delay(200);
                }
            }
            else
            {
                while (true)
                {
                    var random = new Random();
                    dc.CurrentWeight = (Int32)random.NextInt64(6000);
                    UpdateState(dc);
                    await Task.Delay(1000);
                }
            }
        }

        public void GoNext()
        {
            //var recipe = new Recipe() { Name = "Fragancia Suave" };
            //var recipeItem = new RecipeItem() { Name = "Alcohol", MaxAllowedWeight = 5000, MinRequiredWeight = 3800, IdealWeight = 4250 };
            //recipe.Items.Add(recipeItem);
            //recipeItem = new RecipeItem() { Name = "Potasio", MaxAllowedWeight = 2000, MinRequiredWeight = 1000, IdealWeight = 1500 };
            //recipe.Items.Add(recipeItem);


        }

        void UpdateState(WeightViewModel vm)
        {
            var dc = DataContext as WeightViewModel;

            if (vm.CurrentWeight < vm.RecipeItem.MinRequiredWeight)
            {
                vm.WeightFgColor = AppSettings.WeightFgColor;
                vm.WeightBgColor = AppSettings.WeightBgColor;
                vm.CanContinue = false;
            }
            else if (vm.CurrentWeight >= vm.RecipeItem.MinRequiredWeight && vm.CurrentWeight <= vm.RecipeItem.MaxAllowedWeight)
            {
                vm.WeightFgColor = AppSettings.IdealWeightFgColor;
                vm.WeightBgColor = AppSettings.IdealWeightBgColor;
                vm.CanContinue = true;
            }
            else
            {
                vm.WeightFgColor = AppSettings.ErrorWeightFgColor;
                vm.WeightBgColor = AppSettings.ErrorWeightBgColor;
                vm.CanContinue = false;
            }
        }
    }
}
