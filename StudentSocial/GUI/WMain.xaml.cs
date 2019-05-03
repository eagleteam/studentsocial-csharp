using StudentSocial.Common;
using StudentSocial.Model;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WF = System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;
using Application = System.Windows.Application;
using ContextMenu = System.Windows.Controls.ContextMenu;
using MessageBox = System.Windows.MessageBox;

namespace StudentSocial.GUI
{
    /// <summary>
    /// Interaction logic for WMain.xaml
    /// </summary>
    public partial class WMain : Window
    {
        public WMain()
        {
            InitializeComponent();
        }
        private bool hide = false;
        private MediaPlayer mediaPlayer = new MediaPlayer();
        WF.NotifyIcon notifyIcon = new WF.NotifyIcon()
        {
            Icon = new Icon(Application.GetResourceStream(new Uri("pack://application:,,,/Image/ssicon.ico")).Stream),
            Text = "Student Social",
        };
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //CHECK APP RUNING TASK
            //Hiện đăng nhập khi lần đầu
            if (!Paths.checkToken())
            {
                btnLogout.Visibility = Visibility.Collapsed;
                viewMain.Content = new PLogin();
            }
            else
            {
                btnLogout.Visibility = Visibility.Visible;
                /*new Thread(new ThreadStart(*/
                Commons.readDataToFile();/*)).Start();*/
                tbClass.Text = Commons.profile.Lop;
                tbName.Text = Commons.profile.HoTen;
                viewMain.Content = new PTime();
                //Thông báo lịch học ngày mai
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0,0,1);
                timer.Tick += Timer_Tick;
                if (File.ReadAllText(Paths.thongbao) == "true")
                {
                    timer.Start();
                }
                addMenuNoti();
                notifyIcon.Visible = true;
            }
        }
        //MENU
        private void addMenuNoti()
        {
            this.notifyIcon.MouseDown += new WF.MouseEventHandler(notifyIcon_MouseDown);
            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
            notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
            hide = false;
            mediaPlayer.Stop();
        }
        private void NotifyIcon_MouseDoubleClick(object sender, WF.MouseEventArgs e)
        {
            this.Show();
            hide = false;
        }

        void notifyIcon_MouseDown(object sender, WF.MouseEventArgs e)
        {
            if (e.Button == WF.MouseButtons.Right)
            {
                var menu = (ContextMenu) this.FindResource("cmNoti");
                menu.IsOpen = true;
            }
        }
        //END MENU
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (File.Exists(Paths.noti))
            {
                if (DateTime.Now.Hour >= 20 && File.ReadAllText(Paths.noti) == "")
                {
                    thongBaoLich();
                    File.WriteAllText(Paths.noti, "ok");
                }
                if (DateTime.Now.Hour >= 1 && DateTime.Now.Hour <= 19)
                {
                    File.WriteAllText(Paths.noti, "");
                }
            }
        }

        private void thongBaoLich()
        {
            var date = DateTime.Now;
            int count = 0;
            string content = "";
            if (Commons.lstLichHoc.Count > 1)
            {
                int soMon = 1;
                for (int i = 0; i < Commons.lstLichHoc.Count; i++)
                {
                    Lich lich = Commons.lstLichHoc[i] as Lich;
                    var ngay = Convert.ToDateTime(lich.Ngay);
                    if (date.AddDays(1).Date == ngay.Date)
                    {
                        count++;
                        if (lich.LoaiLich == "LichHoc")
                        {
                            content += "(" + soMon++ + ") " + Commons.dicMonHoc[lich.MaMon] + "\n"
                                + "   Tiết " + lich.ThoiGian + " tại " + lich.DiaDiem + "\n";
                            //+ "GV: " + lich.GiaoVien+ "\n";
                        }
                        else
                        {
                            content += "KIỂM TRA: " + Commons.dicMonHoc[lich.MaMon] + "\n"
                            + "   " + lich.ThoiGian + " tại " + lich.DiaDiem + "\n"
                            + "   Hình thức: " + lich.HinhThuc + "\n"
                            + "   SBD: " + lich.SoBaoDanh;
                        }
                    }
                }
            }
            //MEDIA
            var amthanh = File.ReadAllText(Paths.amthanh);
            if (amthanh == "voice")
            {
                if (count == 0) mediaPlayer.Open(new Uri(Paths.audio+"/ranh.mp3", UriKind.Relative));
                if (count == 1) mediaPlayer.Open(new Uri(Paths.audio + "/1.mp3", UriKind.Relative));
                if (count == 2) mediaPlayer.Open(new Uri(Paths.audio + "/2.mp3", UriKind.Relative));
                if (count == 3) mediaPlayer.Open(new Uri(Paths.audio + "/3.mp3", UriKind.Relative));
                if (count == 4) mediaPlayer.Open(new Uri(Paths.audio + "/4.mp3", UriKind.Relative));
            }
            else if (amthanh != "default")
            {
                if (count != 0)
                {
                    mediaPlayer.Open(new Uri(amthanh.Substring(0,amthanh.IndexOf(";"))));
                }
                else
                {
                    mediaPlayer.Open(new Uri(amthanh.Substring(amthanh.IndexOf(";")+1)));
                }
            }
            //END MEDIA
            if (count == 0)
            {
                content = "Ngày mai bạn rảnh!";
            }
            string title = "Student Social: ";
            title += count >= 1 ? "Ngày mai bạn có " + count + " lịch" : "Ngày mai";
            mediaPlayer.Play();
            notifyIcon.ShowBalloonTip(5000, title, content, WF.ToolTipIcon.Info);
        }

        private void LboxMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var token = File.ReadAllText(Paths.token);
            if (token == "" || token == "false")
            {
                MessageBox.Show("Vui lòng đăng nhập trước!",
                    "Thông báo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Stop);
                return;
            }
            var menu = (ListBoxItem)lboxMenu.SelectedItem;
            switch (menu.Tag.ToString())
            {
                case "time":
                    viewMain.Content = new PTime();
                    break;
                case "mark":
                    viewMain.Content = new PMark();
                    break;
                case "update":
                    viewMain.Content = new PUpdate();
                    // lboxMenu.IsEnabled = false;
                    break;
                case "notifb":
                    viewMain.Content = new PNotiFacbook();
                    break;
                case "support":
                    viewMain.Content = new PSupport();
                    break;
                case "logout":
                    kiemTraLogout();
                    break;
                case "setting":
                    viewMain.Content = new PSetting();
                    break;
                default:
                    break;
            }
        }

        private void kiemTraLogout()
        {
            var result = MessageBox.Show("Bạn chắc chắn đăng xuất chứ?\n\r (Dữ liệu sẽ bị mật sau khi bạn đăng xuất nhé!)",
                "Đăng xuất",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                hide = false;
                Paths.clearData();
                Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            else
            {
                lboxMenu.SelectedIndex = 0;
            }
        }
        //đóng
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            if (item.Tag.ToString() == "exit")
            {
                Application.Current.Shutdown();
            }
            else
            {
                mediaPlayer.Stop();
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Show();
            hide = false;
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            if (!hide)
            {
                notifyIcon.ShowBalloonTip(3000, "Thông báo", "Student Social ẩn dưới thanh tác vụ, nhấn đúp để mở.", WF.ToolTipIcon.Info);
                hide = true;
            }
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    mediaPlayer.Open(new Uri("../../Audio/1.mp3", UriKind.Relative));
        //    mediaPlayer.Play();
        //}
    }
}
