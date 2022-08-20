using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Scale.ViewModels
{
    public class WeightViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public double Weight { get; set; }
        double currentWeight;
        public double CurrentWeight
        {
            get { return currentWeight; }
            set
            {
                if (value != currentWeight)
                {
                    currentWeight = value;
                    Dispatcher.UIThread.InvokeAsync(() => NotifyPropertyChanged(nameof(CurrentWeight)));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
