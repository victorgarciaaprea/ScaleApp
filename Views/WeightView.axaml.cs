using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Scale.ViewModels;
using System;
using System.Threading.Tasks;

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

        public async void OnClickChangeWeight(object? sender, RoutedEventArgs args)
        {
            //var dc = DataContext as WeightViewModel;
            //var random = new Random();

            //dc.CurrentWeight = random.NextDouble();

            await ReadWeightFromScale();
            return;
            //var dc = DataContext as MainWindowViewModel;
            //(sender as Button)!.Content = dc.Password;
        }

        public async Task ReadWeightFromScale()
        {
            while (true)
            {
                await Task.Delay(2000);
                var dc = DataContext as WeightViewModel;
                var random = new Random();

                dc.CurrentWeight = random.NextDouble();
            }

        }
    }
}
