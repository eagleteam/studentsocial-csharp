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
using StudentSocial.Common;
namespace StudentSocial.GUI
{
    /// <summary>
    /// Interaction logic for PUpdate.xaml
    /// </summary>
    public partial class PUpdate : Page
    {
        public PUpdate()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbSeme.ItemsSource = Commons.lstKyHoc;
        }

        private void CbSeme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cbSeme.SelectedIndex != -1)
            {
                var seme = cbSeme.SelectedValue.ToString();
                Commons.semesterNow = seme;
                File.WriteAllText(Paths.hocky, seme);
                spnlView.Visibility = Visibility.Visible;
                spnlSelectSeme.Visibility = Visibility.Collapsed;
                new Thread(new ThreadStart(update)).Start();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ký học muốn cập nhật", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        private void update()
        {
            try
            {
                ConnectAPI.getSemester();
                this.Dispatcher.Invoke(() =>
                {
                    lblStatus.Content = "(1) Đang cập nhật lịch học...";
                    lblStatus.Foreground = Brushes.Green;
                });
                ConnectAPI.getTime();
                this.Dispatcher.Invoke(() =>
                {
                    lblStatus.Content = "(2) Đang cập nhật lịch thi...";
                });
                ConnectAPI.getExam();
                this.Dispatcher.Invoke(() =>
                {
                    lblStatus.Content = "(3) Đang cập nhật điểm học phần...";
                });
                ConnectAPI.getMark();
                this.Dispatcher.Invoke(() =>
                {
                    lblStatus.Content = "(OK) Cập nhật hoàn tất! Chuẩn bị khởi động lại...";
                });
                Commons.readDataToFile();
                this.Dispatcher.Invoke(() =>
                {
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                });
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng kiểm tra lại kết nối mạng của bạn rồi thử lại!","Thông báo", MessageBoxButton.OK,MessageBoxImage.Error);
                this.Dispatcher.Invoke(()=> {
                    spnlView.Visibility = Visibility.Collapsed;
                    spnlSelectSeme.Visibility = Visibility.Visible;
                });
            }
        }
    }
}
