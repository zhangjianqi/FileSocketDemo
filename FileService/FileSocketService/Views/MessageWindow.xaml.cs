using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FileSocketService.Views
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
