using FileSocketService.Views;

namespace FileSocketService
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
