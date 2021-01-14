using FileSocketService.ViewModels;
using RRQMSkin.Windows;
using System.Windows;

namespace FileSocketService
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow :RRQMWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Window = this;
            RRQMMVVM.SimpleIoc.Default.Register(this,new MainViewModel());
        }
        public static Window Window;
    }
}
