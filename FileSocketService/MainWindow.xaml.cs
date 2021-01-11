using FileSocketService.ViewModels;
using System.Windows;

namespace FileSocketService
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : RRQMSkin.RRQMWindow
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
