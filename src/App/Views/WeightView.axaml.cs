using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HX711DotNet;
using Scale.Scales;
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
        IScale scale;

        public WeightView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public async void OnClickChangeWeight(object? sender, RoutedEventArgs args)
        {
        }

        private byte dout = 5;
        private byte pd_sck = 6;
        int referenceUnit = 387;

        public async Task ReadWeightFromScale()
        {
            var dc = DataContext as WeightViewModel;
            
            if (dc == null)
            {
                throw new ArgumentNullException("DataConext");
            }

            //if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //{
            //    Pi.Init<BootstrapWiringPi>();
            //    var hx = new HX711(dout, pd_sck);
            //    hx.SetReferenceUnit(referenceUnit);

            //    hx.Reset();
            //    hx.Tare();

            //    while (true)
            //    {
            //        var val = hx.GetWeight(5);
            //        hx.PowerDown();
            //        hx.PowerUp();
            //        Thread.Sleep(100);

            //        if (val >= -2 && val <= 2)
            //            val = 0;

            //        dc.CurrentWeight = val;
            //        UpdateState(dc);
            //        await Task.Delay(200);
            //    }
            //}
            //else
            //{
            //    while (true)
            //    {
            //        var random = new Random();
            //        dc.CurrentWeight = (Int32)random.NextInt64(6000);
            //        UpdateState(dc);
            //        await Task.Delay(1000);
            //    }
            //}
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
