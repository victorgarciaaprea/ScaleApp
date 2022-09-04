using Avalonia;
using Avalonia.ReactiveUI;
using Scale.Models;
using System;
using System.IO;
using System.Text.Json;

namespace Scale
{
    class Program
    {
        internal static ScaleManager ScaleManager = new ScaleManager();

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();


        internal static Recipe GetSampleRecipe()
        {
            var recipe = new Recipe() { Name = "Fragancia Suave" };
            var recipeItem = new RecipeItem() { Name = "Alcohol", MaxAllowedWeight = 5000, MinRequiredWeight = 3800, IdealWeight = 4250, WeightSettings = new WeightSettings() { AllowOverload = true } };
            recipe.Items.Add(recipeItem);
            recipeItem = new RecipeItem() { Name = "Potasio", MaxAllowedWeight = 2000, MinRequiredWeight = 1000, IdealWeight = 1500 };
            recipe.Items.Add(recipeItem);
            return recipe;
        }

        internal static WorkOrderPart GetSampleWorkOrder()
        {
            var content = File.ReadAllText("wo-001.json");
            var wo = JsonSerializer.Deserialize(content, typeof(WorkOrder)) as WorkOrder;
            return wo.Parts[0];
        }

    }
}
