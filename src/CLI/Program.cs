// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Text.Json;
using Scale.Models;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Console.WriteLine("Scale CLI");

        var loadCell1kg = new LoadCell()
        {
            Id = "300GR",
            MaxWeight = 300,
            Brand = "China",
            Description = "Hasta 300 gramos",
            Name = "Precision",
            Model = ""
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
        };

        scale1.LoadCells.Add(loadCell1kg);

        var configText = JsonSerializer.Serialize(scale1);

        File.WriteAllText("scales.json", configText);
    }
}
