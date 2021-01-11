using FileSocketClient.ViewModels;

namespace FileSocketClient
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

        public static MainWindow Window { get; set; }
    }
}
