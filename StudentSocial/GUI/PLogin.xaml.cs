using StudentSocial.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentSocial.GUI
{
    /// <summary>
    /// Interaction logic for PLogin.xaml
    /// </summary>
    public partial class PLogin : Page
    {
        public PLogin()
        {
            InitializeComponent();
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (chkDieuKhoan.IsChecked == false)
            {
                MessageBox.Show("Bạn cần đồng ý với điều khoản của Student Social để tiếp tục sử dụng.",
                    "Thông báo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Stop);
                return;
            }
            lblStatus.Content = "Đang đăng nhập...";
            lblStatus.Foreground = Brushes.Blue;
            new Thread(new ThreadStart(login)).Start();
        }
        private void login()
        {
            var username = "";
            var password = "";
            this.Dispatcher.Invoke(() =>
            {
                username = txtCode.Text;
                password = txtPass.Password;
            });
            username = username.ToUpper();
            var result = ConnectAPI.Login(username, password);
            if (result != "false" && result != "error")
            {
               // File.WriteAllText(Paths.show,"show");
                this.Dispatcher.Invoke(() =>
                {
                    lblStatus.Content = "Đăng nhập thành công!\nĐang chuẩn bị lưu dữ liệu, vui lòng đợi!";
                    lblStatus.Foreground = Brushes.Blue;
                    //Process.Start(Application.ResourceAssembly.Location);
                    //Application.Current.Shutdown();
                });
                ConnectAPI.getProfile();
                this.Dispatcher.Invoke(() =>
                {
                    lblStatus.Content = "(1) Đang tải danh sách kỳ học...";
                    lblStatus.Foreground = Brushes.Green;
                });
                ConnectAPI.getSemester();
                getSemester();
                this.Dispatcher.Invoke(() =>
                {
                    lblStatus.Content = "(2) Đang lưu lịch học...";
                    lblStatus.Foreground = Brushes.Green;
                });
                ConnectAPI.getTime();
                this.Dispatcher.Invoke(() =>
                {
                    lblStatus.Content = "(3) Đang kiểm tra lịch thi...";
                    lblStatus.Foreground = Brushes.Green;
                });
                ConnectAPI.getExam();
                this.Dispatcher.Invoke(() =>
                {
                    lblStatus.Content = "(4) Đang tải điểm học phần...";
                    lblStatus.Foreground = Brushes.Green;
                });
                ConnectAPI.getMark();
                this.Dispatcher.Invoke(() =>
                {
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                });
            }
            else
            {
                if (result == "error")
                {
                    MessageBox.Show("Vui lòng kiểm tra lại kết nối mạng của bạn rồi thử lại!","Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                this.Dispatcher.Invoke(() =>
                {
                    lblStatus.Content = "Đăng nhập thất bại!";
                    lblStatus.Foreground = Brushes.Red;
                });

            }
        }

        private void getSemester()
        {
            Commons.readSemester();
            for (int i = 0; i < Commons.lstKyHoc.Count; i++)
            {
                if (Commons.lstKyHoc[i].KyHienTai == true)
                {
                    Commons.semesterNow = Commons.lstKyHoc[i].MaKy;
                    break;
                }
            }
        }

        private void LblDieuKhoan_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.facebook.com/pg/EagleTeamPage");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           // chkDieuKhoan.IsChecked = true;
        }

        private void ChkDieuKhoan_Checked(object sender, RoutedEventArgs e)
        {
          //  btnLogin.IsEnabled = true;
          //  btnLogin.Foreground = Brushes.White;
        }

        private void ChkDieuKhoan_Unchecked(object sender, RoutedEventArgs e)
        {
           // btnLogin.IsEnabled = false;
           // btnLogin.Foreground = Brushes.Black;
        }
    }
}
