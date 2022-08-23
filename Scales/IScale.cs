using HX711DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

namespace Scale.Scales
{
    public interface IScale
    {
        public void Tare();
        public void Reset();
        public int GetValue();
        public Task Start();
        public event EventHandler ValueChanged;
    }
}
