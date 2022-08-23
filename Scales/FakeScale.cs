using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale.Scales
{
    public class FakeScale : IScale
    {
        public event EventHandler ValueChanged;
        int currentWeight;

        public int GetValue()
        {
            return currentWeight;
        }

        public void Reset()
        {
            return;
        }

        public async Task Start()
        {
            while (true)
            {
                currentWeight = new Random().Next(0, 10000);
                if (ValueChanged != null)
                {
                    ValueChanged(this, new EventArgs());
                }
                await Task.Delay(1000);

            }
        }

        public void Tare()
        {
            currentWeight = 0;
        }
    }
}
