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
    public class Hx711BasedScale : IScale
    {
        private byte dout;
        private byte pd_sck;
        int referenceUnit;
        int currentWeight;
        public event EventHandler ValueChanged;
        HX711 hx;

        public Hx711BasedScale(byte dout, byte pd_sck, int referenceUnit)
        {
            this.dout = dout;
            this.pd_sck = pd_sck;
            this.referenceUnit = referenceUnit;
        }

        public async Task Start()
        {
            Pi.Init<BootstrapWiringPi>();
            hx = new HX711(dout, pd_sck);
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

                currentWeight = val;
                ValueChanged(this, new EventArgs());

                //dc.CurrentWeight = val;
                //UpdateState(dc);
                await Task.Delay(200);

            }

        }

        public int GetValue()
        {
            return currentWeight;
        }

        public void Reset()
        {
            hx.Reset();
        }

        public void Tare()
        {
            hx.Tare();
        }
    }

}
