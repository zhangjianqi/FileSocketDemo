using Microsoft.Win32;
using RRQMMVVM;
using RRQMSocket;
using FileSocketClient.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using RRQMSocket.FileTransfer;

namespace FileSocketClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {

            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            ConnectServiceCommand = new ExecuteCommand(ConnectService);
            DisConnectServiceCommand = new ExecuteCommand(DisConnectService);
            BeginDownloadCommand = new ExecuteCommand(BeginDownload);
            PauseOrResumeDownloadCommand = new ExecuteCommand(PauseOrResumeDownload);
            StopDownloadCommand = new ExecuteCommand(StopDownload);
            SelectUploadFileCommand = new ExecuteCommand(SelectUploadFile);
            BeginUploadFileCommand = new ExecuteCommand(BeginUploadFile);
            PauseOrResumeUploadCommand = new ExecuteCommand(PauseOrResumeUpload);
            StopUploadCommand = new ExecuteCommand(StopUpload);
            ClientSendCommand = new ExecuteCommand(ClientSend);
        }


        #region 变量

        private FileClient fileClient;
        private bool isPauseDownload;
        private bool isPauseUpload;
        #endregion

        #region Command

        public ExecuteCommand ConnectServiceCommand { get; set; }
        public ExecuteCommand DisConnectServiceCommand { get; set; }
        public ExecuteCommand BeginDownloadCommand { get; set; }
        public ExecuteCommand PauseOrResumeDownloadCommand { get; set; }
        public ExecuteCommand StopDownloadCommand { get; set; }
        public ExecuteCommand SelectUploadFileCommand { get; set; }
        public ExecuteCommand BeginUploadFileCommand { get; set; }
        public ExecuteCommand PauseOrResumeUploadCommand { get; set; }
        public ExecuteCommand StopUploadCommand { get; set; }
        public ExecuteCommand ClientSendCommand { get; set; }

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



        private string downloadUrl;

        public string DownloadUrl
        {
            get { return downloadUrl; }
            set { downloadUrl = value; OnPropertyChanged(); }
        }

        private double speed;

        public double Speed
        {
            get { return speed; }
            set { speed = value; OnPropertyChanged(); }
        }

        private double progress;

        public double Progress
        {
            get { return progress; }
            set { progress = value; OnPropertyChanged(); }
        }

        private string uploadUrl;

        public string UploadUrl
        {
            get { return uploadUrl; }
            set { uploadUrl = value; OnPropertyChanged(); }
        }

        private bool isRestartDownload;

        public bool IsRestartDownload
        {
            get { return isRestartDownload; }
            set { isRestartDownload = value; OnPropertyChanged(); }
        }

        private bool isRestartUpload;

        public bool IsRestartUpload
        {
            get { return isRestartUpload; }
            set { isRestartUpload = value; OnPropertyChanged(); }
        }

        private string clientSendText = "测试";

        public string ClientSendText
        {
            get { return clientSendText; }
            set { clientSendText = value; OnPropertyChanged(); }
        }

        private string clientMessageBoxText;

        public string ClientMessageBoxText
        {
            get { return clientMessageBoxText; }
            set { clientMessageBoxText = value; OnPropertyChanged(); }
        }

        #endregion

        #region 公共方法
        #endregion

        #region 绑定方法

        private void ClientSend()
        {
            if (fileClient != null && fileClient.Connected)
            {
                fileClient.SendSystemMessage(this.ClientSendText);
            }
        }

        private void StopUpload()
        {
            if (fileClient != null && fileClient.TransferType == TransferType.Upload)
            {
                fileClient.StopUpload();
                Log.Show("已停止下载");
            }
        }
        private void PauseOrResumeUpload()
        {
            if (fileClient != null && fileClient.TransferType == TransferType.Upload)
            {
                if (this.isPauseUpload)
                {
                    fileClient.ResumeUpload();
                }
                else
                {
                    fileClient.PauseUpload();
                }
                this.isPauseUpload = !this.isPauseUpload;
            }
        }
        private void BeginUploadFile()
        {
            if (fileClient != null && fileClient.Connected)
            {
                FileUrl url = new FileUrl();
                url.FilePath = this.UploadUrl;//对上传文件进行赋值
                url.Restart = this.IsRestartUpload;//标识是否重新传输，为true时不进行断点续传

                if (File.Exists(this.UploadUrl))
                {
                    try
                    {
                        fileClient.UploadFile(url);
                    }
                    catch (Exception e)
                    {
                        Log.Show(e.Message);
                    }

                }
                else
                {
                    Log.Show("文件不存在");
                }

            }
        }
        private void SelectUploadFile()
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            if (fileDialog.FileName != null && fileDialog.FileName.Length > 0)
            {
                this.UploadUrl = fileDialog.FileName;
            }
        }
        private void StopDownload()
        {
            if (fileClient != null && fileClient.TransferType == TransferType.Download)
            {
                fileClient.StopDownload();
                Log.Show("停止当前成功");
            }
        }
        private void PauseOrResumeDownload()
        {
            if (fileClient != null && fileClient.TransferType == TransferType.Download)
            {
                if (this.isPauseDownload)
                {
                    fileClient.ResumeDownload();
                }
                else
                {
                    fileClient.PauseDownload();
                }
                this.isPauseDownload = !this.isPauseDownload;
            }
        }


        private void ConnectService()
        {
            if (fileClient != null)
            {
                Log.Show("请勿重复连接");
                return;
            }

            fileClient = new FileClient();

            ConnectSetting setting = new ConnectSetting();
            setting.TargetIP = "127.0.0.1";
            setting.TargetPort = 7788;
            setting.MultithreadThreadCount = 2;
            try
            {
                fileClient.Connect(setting);

                fileClient.TransferFileError += FileClient_TransferFileError;
                fileClient.BeforeDownloadFile += FileClient_BeforeDownloadFile;
                fileClient.DownloadFileFinshed += FileClient_DownloadFileFinshed;
                fileClient.UploadFileFinshed += FileClient_UploadFileFinshed;
                fileClient.ReceiveSystemMes += FileClient_ReceiveSystemMes;
                fileClient.DisConnectedService += FileClient_DisConnectedService;
                this.ClientIconForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6CE26C"));
                Log.Show("连接成功");
            }
            catch (Exception ex)
            {
                if (fileClient != null && fileClient.Connected)
                {
                    fileClient.Dispose();
                }

                fileClient = null;
                this.ClientIconForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8EC"));
                Log.Show(ex.Message);
            }

        }

       
        private void DisConnectService()
        {
            if (fileClient != null && fileClient.Connected)
            {
                fileClient.Dispose();
            }

            fileClient = null;
            this.ClientIconForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8EC"));
        }

        private void BeginDownload()
        {
            if (fileClient != null && fileClient.Connected)
            {
                FileUrl url = new FileUrl();
                url.FilePath = this.DownloadUrl;//这里对请求下载文件路径赋值
                url.Restart = this.IsRestartDownload;//标识是否重新传输，为true时不进行断点续传

                try
                {
                    fileClient.DownloadFile(url);
                }
                catch (Exception e)
                {
                    Log.Show(e.Message);
                }
              
            }
        }
        #endregion

        #region 事件方法


        private void FileClient_DisConnectedService(object sender, MesEventArgs e)
        {
            if (fileClient != null)
            {
                fileClient.Dispose();
            }

            fileClient = null;
            UIInvoke(() =>
            {
                this.ClientIconForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8EC"));

            });
        }

        private void FileClient_UploadFileFinshed(object sender, FileFinishedArgs e)
        {
            Log.Show(string.Format("文件：{0}上传完成", e.FileInfo.FileName));
        }

        private void FileClient_DownloadFileFinshed(object sender, FileFinishedArgs e)
        {
            FileClient fileClient = sender as FileClient;//客户端中事件的sender实例均为FileClient
            RRQMSocket.FileTransfer.FileInfo fileInfo = e.FileInfo;//通过事件参数值，可获得完成下载的文件信息
            Log.Show(string.Format("文件：{0}下载完成", e.FileInfo.FileName));
        }

        private void FileClient_BeforeDownloadFile(object sender, TransferFileEventArgs e)
        {
            if (!Directory.Exists("ClientReceiveDir"))
            {
                Directory.CreateDirectory("ClientReceiveDir");
            }
            e.TargetPath = @"ClientReceiveDir\" + e.FileInfo.FileName;
        }

        private void FileClient_ReceiveSystemMes(object sender, MesEventArgs e)
        {
            UIInvoke(() =>
            {
                this.ClientMessageBoxText = this.ClientMessageBoxText + string.Format("收到服务器发来的消息：{0}\r\n", e.Message);
            });
        }

        private void FileClient_TransferFileError(object sender, TransferFileMessageArgs e)
        {

            Log.Show(e.Message);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UIInvoke(() =>
                  {
                      if (fileClient != null)
                      {
                          if (fileClient.TransferType == TransferType.Download)
                          {
                              this.Speed = fileClient.DownloadSpeed / 1024;
                              this.Progress = fileClient.DownloadProgressValue;
                          }
                          else if (fileClient.TransferType == TransferType.Upload)
                          {
                              this.Speed = fileClient.UploadSpeed / 1024;
                              this.Progress = fileClient.UploadProgressValue;
                          }
                          else
                          {
                              this.Speed = 0;
                              this.Progress = 0;
                          }


                      }
                      else
                      {
                          this.Speed = 0;
                          this.Progress = 0;
                      }
                  });
        }

        #endregion
    }
}
