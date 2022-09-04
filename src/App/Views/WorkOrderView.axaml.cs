using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MessageBox.Avalonia.DTO;
using Scale.Models;
using Scale.ViewModels;

namespace Scale.Views
{
    public partial class WorkOrderView : UserControl
    {
        public WorkOrderView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }


        public void bt_SearchWorkOrder(object? sender, RoutedEventArgs args)
        {
            var dc = DataContext as WorkOrderViewModel;

            var man = new WorkOrderManager();

            var wo = man.Find(dc.Id);

            if (wo == null)
            {
                new MessageBoxDialog().ShowError("Orden de Trabajo Inexistente", "Por favor compruebe el numero");
                return;
            }
            else
            {
                if (!CheckForStatus(wo.Status))
                {
                    return;
                }

                var woPart = man.GetNextPart(wo);
                if (woPart == null)
                {
                    new MessageBoxDialog().ShowError("Orden de Trabajo Corrupta", "No se puede seguir");
                    return;
                }

                if (!Program.ScaleManager.IsScaleSupported(woPart.ScaleId))
                {
                    new MessageBoxDialog().ShowError("Balanza no compatible", $"Se require: {woPart.ScaleId}");
                    return;
                }


                var parent = this.Parent as MainWindow;

                parent.NavigateTo(new WeightViewModel(woPart));
            }


            //(sender as Button)!.Content = dc.Password;
        }

        bool CheckForStatus(WorkOrderStatus status)
        {
            if (status == Models.WorkOrderStatus.Failed)
            {
                new MessageBoxDialog().ShowError("Orden de Trabajo Fallida", "No puede seguir trabajando en la misma");
                return false;
            }
            else if (status == Models.WorkOrderStatus.Completed)
            {
                new MessageBoxDialog().ShowError("Orden de Trabajo Terminada", "Esta Orden ya fue completada");
                return false;
            }
            else if (status == Models.WorkOrderStatus.Aborted)
            {
                new MessageBoxDialog().ShowError("Orden de Trabajo Abortada", "Esta Orden fue abortada");
                return false;
            }
            else if (status == Models.WorkOrderStatus.Deleted)
            {
                new MessageBoxDialog().ShowError("Orden de Trabajo Eliminada", "Esta Orden fue eliminada");
                return false;
            }

            return true;
        }


    }
}
