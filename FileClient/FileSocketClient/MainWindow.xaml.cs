using FileSocketClient.ViewModels;
using RRQMSkin.Windows;

namespace FileSocketClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : RRQMWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Window = this;
        }

        public static MainWindow Window { get; set; }
    }
}
