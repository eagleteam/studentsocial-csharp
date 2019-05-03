using Microsoft.Win32;
using StudentSocial.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
using Path = System.IO.Path;

namespace StudentSocial.GUI
{
    /// <summary>
    /// Interaction logic for PSetting.xaml
    /// </summary>
    public partial class PSetting : Page
    {
        public PSetting()
        {
            InitializeComponent();
        }

        private void ChkStart_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            box.Content = "On";
            if (box.Tag.ToString() == "khoidong")
            {
                File.WriteAllText(Paths.khoidong, "true");
                try
                {
                    Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    key.SetValue("Student Social WPF", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\VSond Soft\Student Social WPF\StudentSocial.exe"));
                }
                catch (Exception)
                {
                }

            }
            if (box.Tag.ToString() == "thongbao")
            {
                File.WriteAllText(Paths.thongbao, "true");
                spnlAmThanh.Visibility = Visibility.Visible;
                khoidong.Visibility = Visibility.Visible;

            }
        }

        private void ChkStart_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            box.Content = "Off";
            if (box.Tag.ToString() == "khoidong")
            {
                File.WriteAllText(Paths.khoidong, "false");
                try
                {
                    Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    key.DeleteValue("Student Social WPF", false);
                }
                catch (Exception)
                {
                }
            }
            if (box.Tag.ToString() == "thongbao")
            {
                khoidong.Visibility = Visibility.Visible;
                File.WriteAllText(Paths.thongbao, "false");
                spnlAmThanh.Visibility = Visibility.Collapsed;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.ReadAllText(Paths.khoidong) == "true")
            {
                chkStart.IsChecked = true;
            }
            else
            {
                chkStart.IsChecked = false;
            }

            if (File.ReadAllText(Paths.thongbao) == "true")
            {
                chkNoti.IsChecked = true;
            }
            else
            {
                chkNoti.IsChecked = false;
                spnlAmThanh.Visibility = Visibility.Collapsed;
            }
            var amthanh = File.ReadAllText(Paths.amthanh);
            if (amthanh == "default")
            {
                radMacDinh.IsChecked = true;
                spnlChonFile.Visibility = Visibility.Collapsed;
            }
            else if (amthanh == "voice")
            {
                radGiongNoi.IsChecked = true;
                spnlChonFile.Visibility = Visibility.Collapsed;
            }
            else
            {
                radTuyChinh.IsChecked = true;
                spnlChonFile.Visibility = Visibility.Visible;
                txtAmThanh.Text = amthanh.Substring(0,amthanh.IndexOf(";"));
                txtAmThanh2.Text = amthanh.Substring(amthanh.IndexOf(";")+1);
            }
        }

        private void RadMacDinh_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rad = sender as RadioButton;
            if (rad.Tag.ToString() == "macdinh")
            {
                File.WriteAllText(Paths.amthanh, "default");
                spnlChonFile.Visibility = Visibility.Collapsed;
            }
            if (rad.Tag.ToString() == "giongnoi")
            {
                File.WriteAllText(Paths.amthanh, "voice");
                spnlChonFile.Visibility = Visibility.Collapsed;
            }
            if (rad.Tag.ToString() == "tuychinh")
            {
                spnlChonFile.Visibility = Visibility.Visible;
            }
        }

        private void RadMacDinh_Unchecked(object sender, RoutedEventArgs e)
        {
            //RadioButton rad = sender as RadioButton;
            //if (rad.Tag.ToString() == "macdinh")
            //{
            //    File.WriteAllText(Paths.amthanh, "default");
            //}
            //if (rad.Tag.ToString() == "giongnoi")
            //{
            //    File.WriteAllText(Paths.amthanh, "voice");
            //}
            //if (rad.Tag.ToString() == "tuychinh")
            //{
            //    File.WriteAllText(Paths.amthanh, "default");
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.DefaultExt = "mp3";
            openFile.Filter = "mp3 files (*.mp3)|*.mp3";
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            openFile.ShowDialog();
            if (openFile.FileName != "")
            {
                if (btn.Tag.ToString() == "hoc")
                {
                    txtAmThanh.Text = openFile.FileName;
                }
                else
                {
                    txtAmThanh2.Text = openFile.FileName;
                }
            }
            File.WriteAllText(Paths.amthanh, txtAmThanh.Text+";"+txtAmThanh2.Text);
        }

        private void Khoidong_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
