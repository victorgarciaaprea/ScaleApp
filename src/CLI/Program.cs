// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Scale;
using Scale.Models;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Console.WriteLine("Scale CLI");

        WriteScalesData();
        WriteWorkOrderData();

    }

    static void WriteWorkOrderData()
    {
        var wo = new WorkOrder();
        wo.Id = Guid.NewGuid().ToString();
        wo.Status = WorkOrderStatus.NotStarted;
        wo.ExternalId = "ot-2392";
        wo.Parts = new List<WorkOrderPart>();
        var part = new WorkOrderPart();
        part.ScaleId = "Ipeco01";

        var recipeItem = new RecipeItem() { Id= "40001A1", Name = "Alcohol", MaxAllowedWeight = 2000, MinRequiredWeight = 1700, IdealWeight = 1850, WeightSettings = new WeightSettings() { AllowOverload = true } };
        part.Items.Add(recipeItem);
        recipeItem = new RecipeItem() { Id= "40002B5", Name = "Potasio", MaxAllowedWeight = 4000, MinRequiredWeight = 3500, IdealWeight = 3750 };
        part.Items.Add(recipeItem);

        wo.Parts.Add(part);

        var configText = JsonSerializer.Serialize(wo);
        File.WriteAllText("wo-001.json", configText);


    }
    static void WriteScalesData()
    {

        var scales = new List<Scale.Models.Scale>();

        var loadCell1kg = new LoadCell()
        {
            Id = "300GR",
            MaxWeight = 300,
            Brand = "China",
            Description = "Hasta 300 gramos",
            Name = "Precision",
            Model = ""
        };

        var loadCell5kg = new LoadCell()
        {
            Id = "5KG",
            MaxWeight = 5000,
            Brand = "IPeCo",
            Description = "Hasta 5000 gramos",
            Name = "TP",
            Model = "50"
        };

        var scale1 = new Scale.Models.Scale()
        {
            Name = "Precision",
            Description = "Balanza de precision, hasta 300 gramos",
            MaxWeight = 300,
            MinWeight = 0,
            Id = "Precision01",
            LastCalibration = DateTime.Now,
            ClockLine = 0,
            DataLine = 0,
            AdjustFactor = 0,
            Tolerance = 0,
            Type = "Prec"
        };

        var scale2 = new Scale.Models.Scale()
        {
            Name = "IPeCo",
            Description = "Balanza hasta 5kg",
            MaxWeight = 5000,
            MinWeight = 0,
            Id = "Ipeco01",
            LastCalibration = DateTime.Now,
            ClockLine = 0,
            DataLine = 0,
            AdjustFactor = 0,
            Tolerance = 0,
            Type = "Std"
        };

        scale1.LoadCells.Add(loadCell1kg);
        scale2.LoadCells.Add(loadCell5kg);

        scales.Add(scale1);
        scales.Add(scale2);

        var configText = JsonSerializer.Serialize(scales);

        File.WriteAllText("scales.json", configText);
    }
}

