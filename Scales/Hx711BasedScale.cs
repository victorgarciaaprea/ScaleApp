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
    public class Hx711BasedScale : ScaleBase
    {
        private byte dout;
        private byte pd_sck;
        int referenceUnit;
        HX711 hx;
        CancellationTokenSource cancellationTokenSource;

        public Hx711BasedScale(byte dout, byte pd_sck, int referenceUnit)
        {
            this.dout = dout;
            this.pd_sck = pd_sck;
            this.referenceUnit = referenceUnit;
        }

        public async void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken ct = cancellationTokenSource.Token;
            await StartInternal(ct);
        }

        public override void Stop()
        {

            cancellationTokenSource.Cancel();
        }

        public async Task StartInternal(CancellationToken ct)
        {
            Pi.Init<BootstrapWiringPi>();
            hx = new HX711(dout, pd_sck);
            hx.SetReferenceUnit(referenceUnit);

            hx.Reset();
            hx.Tare();

            while (true)
            {
                if (ct.IsCancellationRequested)
                {
                    break;
                }

                var val = hx.GetWeight(5);
                hx.PowerDown();
                hx.PowerUp();
                Thread.Sleep(100);

                if (val >= -2 && val <= 2)
                    val = 0;

                OnValueChanged(val);

                //dc.CurrentWeight = val;
                //UpdateState(dc);
                await Task.Delay(200);

            }

        }

        public override void Reset()
        {
            hx.Reset();
        }

        public override void Tare()
        {
            hx.Tare();
        }
    }

}
