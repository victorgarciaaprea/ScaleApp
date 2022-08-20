using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Scale.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }


        public void bt_Login(object? sender, RoutedEventArgs args)
        {

            //var dc = DataContext as MainWindowViewModel;
            //(sender as Button)!.Content = dc.Password;
        }
    }
}
