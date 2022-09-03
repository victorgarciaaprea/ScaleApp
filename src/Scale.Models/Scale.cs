using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale.Models
{
    public class Scale
    {
        public Scale()
        {
            LoadCells = new List<LoadCell>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public double AdjustFactor { get; set;}
        public double MinWeight { get; set;}
        public double MaxWeight { get; set; }
        public double Tolerance { get; set;}
        public DateTime LastCalibration { get; set; }
        public List<LoadCell> LoadCells { get; set; }
        // These are used for HX711 and any others I2C-like protocols
        public int ClockLine { get; set; }
        public int DataLine { get; set; }

    }
}
