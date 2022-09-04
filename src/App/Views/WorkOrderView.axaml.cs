using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MessageBox.Avalonia.DTO;
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

            var wo = Program.SearchForWorkOrder(dc.Id);
            if (wo == null)
            {
                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                               new MessageBoxStandardParams
                               {
                                   ContentTitle = "Error",
                                   ContentHeader = "Orden de Trabajo Invalida",
                                   ContentMessage = "Por favor verifique el numero ingresado",
                                   CanResize = false
                               });
                messageBoxStandardWindow.Show();
            }
            else
            {

                var parent = this.Parent as MainWindow;
                parent.NavigateTo(new WeightViewModel());
            }


            //(sender as Button)!.Content = dc.Password;
        }
    }
}
