using MessageBox.Avalonia.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale
{
    internal class MessageBoxDialog
    {
        private void ShowMessage(string title, string header, string message)
        {
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                           new MessageBoxStandardParams
                           {
                               ContentTitle = title,
                               ContentHeader = header,
                               ContentMessage = message,
                               CanResize = false
                           });
            messageBoxStandardWindow.Show();
        }

        internal void ShowError(string header, string message)
        {
            ShowMessage("Error", header, message);
        }
    }
}
