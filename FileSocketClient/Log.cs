using FileSocketClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSocketClient
{
  public static  class Log
    {
        public static void Show(string mes)
        {
            MainWindow.Window.Dispatcher.Invoke(()=> 
            {
                MessageWindow messageWindow = new MessageWindow(mes);
                messageWindow.Owner = MainWindow.Window;
                messageWindow.Show();
            });
            
        }
    }
}
