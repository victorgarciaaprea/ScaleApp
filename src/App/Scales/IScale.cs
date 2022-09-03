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
        public int CurrentWeight { get; }
        public void Tare();
        public void Reset();
        public void Start();
        public void Stop();
        public Task StartInternal(CancellationToken cancellationToken);
        public event EventHandler ValueChanged;
    }

    public abstract class ScaleBase : IScale
    {
        CancellationTokenSource cancellationTokenSource;
        public event EventHandler ValueChanged;
        int currentWeight;

        public abstract void Reset();
        public abstract void Tare();

        public int CurrentWeight { get { return currentWeight; } }

        public virtual void OnValueChanged(int newValue)
        {
            currentWeight = newValue;

            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

        public async virtual void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken ct = cancellationTokenSource.Token;
            await StartInternal(ct);
        }

        public virtual void Stop()
        {
            cancellationTokenSource.Cancel();
        }

        public virtual Task StartInternal(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
