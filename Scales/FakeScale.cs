using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scale.Scales
{
    public class FakeScale : ScaleBase
    {
        public override void Reset()
        {
            return;
        }

        public async override Task StartInternal(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                var newValue = new Random().Next(0, 10000);
                OnValueChanged(newValue);
                await Task.Delay(1000);
            }
        }

        public override void Tare()
        {
            OnValueChanged(0);
        }

    }
}
