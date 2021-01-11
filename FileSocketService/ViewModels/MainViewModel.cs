using RRQMMVVM;
using RRQMSocket;
using RRQMSocket.FileTransfer;
using System.IO;
using System.Timers;
using System.Windows.Media;

namespace FileSocketService.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            this.ClientItems = new RRQMList<FileSocketClient>();
            this.CreatServiceCommand = new ExecuteCommand(CreatService);
            this.CloseServiceCommand = new ExecuteCommand(CloseService);
            this.ServiceSendCommand = new ExecuteCommand(ServiceSend);
        }

        #region 变量

        private FileService fileService;

        #endregion

        #region Command
        public ExecuteCommand CreatServiceCommand { get; set; }
        public ExecuteCommand CloseServiceCommand { get; set; }
        public ExecuteCommand ServiceSendCommand { get; set; }
        #endregion

        #region 属性


        private Brush serviceIconForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8EC"));

        public Brush ServiceIconForeground
        {
            get { return serviceIconForeground; }
            set { serviceIconForeground = value; OnPropertyChanged(); }
        }

        private Brush clientIconForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8EC"));

        public Brush ClientIconForeground
        {
            get { return clientIconForeground; }
            set { clientIconForeground = value; OnPropertyChanged(); }
        }


        private string tip1 = "所有者信息：未知";

        public string Tip1
        {
            get { return tip1; }
            set { tip1 = value; OnPropertyChanged(); }
        }

        private string tip2 = "使用期限：未知";

        public string Tip2
        {
            get { return tip2; }
            set { tip2 = value; OnPropertyChanged(); }
        }

        private string tip3 = "最大连接数：未知";

        public string Tip3
        {
            get { return tip3; }
            set { tip3 = value; OnPropertyChanged(); }
        }

        private RRQMList<FileSocketClient> clientItems;

        public RRQMList<FileSocketClient> ClientItems
        {
            get { return clientItems; }
            set { clientItems = value; OnPropertyChanged(); }
        }

        private bool allowDownload = true;

        public bool AllowDownload
        {
            get { return allowDownload; }
            set { allowDownload = value; OnPropertyChanged(); }
        }

        private bool allowUpload = true;

        public bool AllowUpload
        {
            get { return allowUpload; }
            set { allowUpload = value; OnPropertyChanged(); }
        }

        private FileSocketClient selectedClient;

        public FileSocketClient SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged();
            }
        }

    

        private string serviceSendText;

        public string ServiceSendText
        {
            get { return serviceSendText; }
            set { serviceSendText = value; OnPropertyChanged(); }
        }

        private string serviceMessageBoxText;

        public string ServiceMessageBoxText
        {
            get { return serviceMessageBoxText; }
            set { serviceMessageBoxText = value; OnPropertyChanged(); }
        }


        #endregion

        #region 公共方法
        #endregion

        #region 绑定方法
        private void ServiceSend()
        {
            if (this.SelectedClient != null)
            {
                this.SelectedClient.SendSystemMessage(this.ServiceSendText);
            }
        }

        private void CreatService()
        {
            if (fileService != null && fileService.IsBind)
            {
                Log.Show("请勿重复绑定");
                return;
            }

            fileService = new FileService();
            fileService.IsFsatUpload = true;//快速传输
            fileService.BreakpointResume = true;//断点续传
            try
            {
                BindSetting setting = new BindSetting();
                setting.IP = "127.0.0.1";
                setting.Port =7788;
                setting.MultithreadThreadCount = 1;//多线程，线程数量为2
                fileService.Bind(setting);

                fileService.ClientConnected += FileService_ClientConnected;
                fileService.ClientDisconnected += FileService_ClientDisconnected;
                
                fileService.BeforeReceiveFile += FileService_BeforeReceiveFile;
                fileService.BeforeSendFile += FileService_BeforeSendFile;
                fileService.ReceiveSystemMes += FileService_ReceiveSystemMes;
                fileService.ReceiveFileFinished += FileService_ReceiveFileFinished;
                fileService.SendFileFinished += FileService_SendFileFinished;
                this.Tip1 = string.Format("所有者信息：{0}", fileService.UserMessage);
                this.Tip2 = string.Format("使用期限：{0}", fileService.LicenceDate);
                this.Tip3 = string.Format("最大连接数：{0}", fileService.MaxConnection);

                this.ServiceIconForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6CE26C"));
                Log.Show("启动成功");
            }
            catch (RRQMLicenceKeyException ex)
            {
                this.Tip1 = string.Format("所有者信息：{0}", "无");
                this.Tip2 = string.Format("使用期限：{0}", "无");

                this.ServiceIconForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8EC"));
                Log.Show(ex.Message);
            }

        }


        private void CloseService()
        {
            this.ClientItems.Clear();
            if (fileService != null)
            {
                fileService.Dispose();
                fileService = null;
            }

            this.Tip1 = string.Format("所有者信息：{0}", "无");
            this.Tip2 = string.Format("使用期限：{0}", "无");

            this.ServiceIconForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8EC"));
        }

        #endregion

        #region 事件方法
       
        private void FileService_SendFileFinished(object sender, FileFinishedArgs e)
        {
            UIInvoke(() =>
            {
                this.ServiceMessageBoxText = this.ServiceMessageBoxText + string.Format("{0}请求的文件：{1}已成功发送\r\n", ((FileSocketClient)sender).Name, e.FileInfo.FileName);
            });
        }
        private void FileService_ReceiveFileFinished(object sender, FileFinishedArgs e)
        {
            UIInvoke(() =>
            {
                this.ServiceMessageBoxText = this.ServiceMessageBoxText + string.Format("已收到{0}发来的文件：{1}\r\n", ((FileSocketClient)sender).Name, e.FileInfo.FileName);
            });
        }

        private void FileService_ReceiveSystemMes(object sender, MesEventArgs e)
        {
            UIInvoke(() =>
            {
                this.ServiceMessageBoxText = this.ServiceMessageBoxText + string.Format("收到{0}发来的消息：{1}\r\n", ((FileSocketClient)sender).Name, e.Message);
            });
        }


        private void FileService_ClientConnected(object sender, MesEventArgs e)
        {
            
            FileSocketClient client = (FileSocketClient)sender;
            UIInvoke(() =>
            {
                this.ClientItems.Add(client);
            });

        }

        private void FileService_ClientDisconnected(object sender, MesEventArgs e)
        {
            UIInvoke(() =>
            {
                this.ClientItems.Remove((FileSocketClient)sender);
            });
        }

        private void FileService_BeforeReceiveFile(object sender, TransferFileEventArgs e)
        {
            if (!Directory.Exists("ServiceReceiveDir"))
            {
                Directory.CreateDirectory("ServiceReceiveDir");
            }
            e.TargetPath = @"ServiceReceiveDir\" + e.FileInfo.FileName;
            e.IsPermitTransfer = this.AllowUpload;//是否允许接收
         
        }
        private void FileService_BeforeSendFile(object sender, TransferFileEventArgs e)
        {
            e.IsPermitTransfer = this.AllowDownload;//是否允许下载

        }
        #endregion
    }
}
