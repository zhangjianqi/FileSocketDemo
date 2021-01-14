using System;
using System.Timers;
using System.Windows;

namespace FileSocketClient.Views
{
    /// <summary>
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string mes)
        {
            InitializeComponent();
            this.MesBox.Text = mes;
            timer = new Timer(10);
            timer.Elapsed += Timer_Elapsed;
        }

       
        Timer timer;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Enabled = true;

        }

        int i;
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                MainWindow.Window.Dispatcher.Invoke(new Action(() =>
                {

                    if (i > 100)
                    {
                        this.Top -= 6;

                    }

                    i++;
                    this.Opacity -= 0.005;
                    if (i > 150)
                    {
                        this.Close();
                    }

                }));
            }
            catch (Exception)
            {

            }
           
        }
    }
}
