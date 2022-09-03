using ReactiveUI;
using Scale.Models;
using Scale.Views;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;

namespace Scale.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;
        public string Greeting => "Welcome to Avalonia!";


        public MainWindowViewModel()
        {
            Content = new LoginViewModel();
        }

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public async Task AddItem22()
        {
            while (true)
            {
                await Task.Delay(2000);
            }
        }

        //public void GoNext()
        //{
        //    var recipe = new Recipe() { Name = "Fragancia Suave" };
        //    var recipeItem = new RecipeItem() { Name = "Alcohol", MaxAllowedWeight = 5000, MinRequiredWeight = 3800, IdealWeight = 4250 };
        //    recipe.Items.Add(recipeItem);
        //    recipeItem = new RecipeItem() { Name = "Potasio", MaxAllowedWeight = 2000, MinRequiredWeight = 1000, IdealWeight = 1500 };
        //    recipe.Items.Add(recipeItem);


        //    Content = new WeightViewModel(recipe, 1);

        //}
        public void AddItem()
        {
            //int pin = 18;
            //using var controller = new GpioController();
            //controller.OpenPin(pin, PinMode.Output);
            //bool ledOn = true;
            //while (true)
            //{
            //    controller.Write(pin, ((ledOn) ? PinValue.High : PinValue.Low));
            //    Thread.Sleep(1000);
            //    ledOn = !ledOn;
            //}



            Content = new WeightViewModel(Program.GetSampleRecipe(), this);
        }

        public void NavigateTo(ViewModelBase view)
        {
            this.Content = view;
        }
    }
}
