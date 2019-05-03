using StudentSocial.Common;
using StudentSocial.Model;
using System;
using System.Collections.Generic;
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

namespace StudentSocial.GUI
{
    /// <summary>
    /// Interaction logic for PTime.xaml
    /// </summary>
    public partial class PTime : Page
    {
        private int day;
        private int month;
        private int year;
        private Label lblNow = new Label();

        public PTime()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            getMonth(DateTime.Now.Month, DateTime.Now.Year);
            loadLich(DateTime.Now.Month, DateTime.Now.Year);
            pushData();
        }

        private void loadLich(int month, int year)
        {
            gCalender.Children.Clear();
            var first = new DateTime(year, month, 1);
            var day = (int)first.DayOfWeek;
            int now = 1;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i == 0 && j >= day - 1)
                    {
                        StackPanel panel = new StackPanel()
                        {

                        };
                        panel.MouseLeftButtonDown += Panel_MouseLeftButtonDown;
                        Label lblDay = new Label()
                        {
                            Cursor = Cursors.Hand,
                            Content = now++,
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            VerticalContentAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Stretch,
                            FontSize = 18,
                            Padding = new Thickness(0, 5, 0, 0),
                        };
                        lblDay.MouseLeftButtonDown += LblDay_MouseLeftButtonDown;
                        lblDay.MouseDoubleClick += LblDay_MouseDoubleClick;
                        panel.Children.Add(lblDay);
                        gCalender.Children.Add(panel);
                        Grid.SetRow(panel, i);
                        Grid.SetColumn(panel, j);
                    }
                    else if (i > 0)
                    {
                        StackPanel panel = new StackPanel();
                        Label lblDay = new Label()
                        {
                            Cursor = Cursors.Hand,
                            Content = now++,
                            //BorderThickness = new Thickness(1),
                            //BorderBrush = Brushes.Green,
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            VerticalContentAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Stretch,
                            FontSize = 18,
                            Padding = new Thickness(0, 5, 0, 0)
                        };
                        lblDay.MouseLeftButtonDown += LblDay_MouseLeftButtonDown;
                        lblDay.MouseDoubleClick += LblDay_MouseDoubleClick;
                        panel.Children.Add(lblDay);
                        gCalender.Children.Add(panel);
                        Grid.SetRow(panel, i);
                        Grid.SetColumn(panel, j);
                    }
                    if (now > DateTime.DaysInMonth(year, month))
                    {
                        break;
                    }
                }
                if (now > DateTime.DaysInMonth(year, month))
                {
                    break;
                }
            }
            //Kiem tra ngay hom nay
            getFullDate();
            if (month == DateTime.Now.Month)
            {
                for (int i = 0; i < gCalender.Children.Count; i++)
                {
                    if (gCalender.Children[i] != null)
                    {
                        StackPanel stack = (StackPanel)gCalender.Children[i];
                        Label lbl = (Label)stack.Children[0];
                        if ((int)lbl.Content == DateTime.Now.Day)
                        {
                            stack.Background = Brushes.Orange;
                            lblNow = lbl;
                        }
                    }
                }
            }
        }

        private void LblDay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lbl = sender as Label;
            //MessageBox.Show("Theem ghi chus "+ day + month + year);
            WNotes notes = new WNotes(day, month, year);
            notes.ShowDialog();
        }

        //Khi Nhấn chuột
        private void LblDay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int soLich = 0;
            spnlView.Children.Clear();
            Label lbl = sender as Label;
            ((StackPanel)lbl.Parent).Background = Brushes.DarkOliveGreen;
            ImageBrush ib = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/select.png")),
                Stretch = Stretch.Uniform,
            };
            ((StackPanel)lbl.Parent).Background = ib;
            lbl.Foreground = Brushes.White;
            //lbl.FontWeight = FontWeights.SemiBold;
            for (int i = 0; i < gCalender.Children.Count; i++)
            {
                if (lbl != ((Label)(((StackPanel)gCalender.Children[i]).Children[0])))
                {
                    ((StackPanel)gCalender.Children[i]).Background = Brushes.White;
                    ((Label)(((StackPanel)gCalender.Children[i]).Children[0])).Foreground = Brushes.Black;
                }
                if (lblNow == ((Label)(((StackPanel)gCalender.Children[i]).Children[0])))
                {
                    ((StackPanel)gCalender.Children[i]).Background = new ImageBrush()
                    {
                        ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/selectnow.png")),
                        Stretch = Stretch.Uniform,
                    }; ;
                }
            }
            getFullDate();
            day = Convert.ToInt32(lbl.Content);
            var date = new DateTime(year, month, day);
            for (int i = 0; i < Commons.lstLichHoc.Count; i++)
            {
                var lich = Commons.lstLichHoc[i];
                var mon = "";
                for (int j = 0; j < Commons.lstMonHoc.Count; j++)
                {
                    if (lich.MaMon == Commons.lstMonHoc[j].MaMon)
                    {
                        mon = Commons.lstMonHoc[j].TenMon;
                        break;
                    }
                }
                if (lich.Ngay == date.ToString("yyyy-MM-dd"))
                {
                    soLich++;
                    if (lich.LoaiLich == "LichHoc")
                    {
                        //Thêm vào lịch hiển thị
                        StackPanel panel = new StackPanel()
                        {
                            Background = rdColor(),
                            Margin = new Thickness(5, 0, 0, 5),
                        };
                        Label tenMon = new Label()
                        {
                            Content = mon,
                            FontSize = 16,
                            FontWeight = FontWeights.Bold,
                            Foreground = Brushes.White
                        };
                        Label thoiGian = new Label()
                        {
                            Content = "Tiết " + lich.ThoiGian + " tại " + lich.DiaDiem,
                            FontSize = 16,
                            Padding = new Thickness(15, 5, 5, 5),
                            Foreground = Brushes.White
                        };
                        Label GV = new Label()
                        {
                            Content = "GV: " + lich.GiaoVien,
                            FontSize = 16,
                            Padding = new Thickness(15, 5, 5, 5),
                            Foreground = Brushes.White
                        };
                        panel.Children.Add(tenMon);
                        panel.Children.Add(thoiGian);
                        panel.Children.Add(GV);
                        spnlView.Children.Add(panel);
                    }
                    else if (lich.LoaiLich == "LichThi")
                    {
                        //Thêm vào lịch hiển thị
                        StackPanel panel = new StackPanel()
                        {
                            Background = Brushes.OrangeRed,
                            Margin = new Thickness(5, 0, 0, 5),

                        };
                        Label tenMon = new Label()
                        {
                            Content = mon,
                            FontSize = 16,
                            FontWeight = FontWeights.Bold,
                            Foreground = Brushes.White
                        };
                        Label thoiGian = new Label()
                        {
                            Content = lich.ThoiGian + " tại " + lich.DiaDiem,
                            FontSize = 16,
                            Foreground = Brushes.White,
                            Padding = new Thickness(15, 5, 5, 5),
                        };
                        Label hinhThuc = new Label()
                        {
                            Content = "Hình thức: " + lich.HinhThuc,
                            FontSize = 16,
                            Foreground = Brushes.White,
                            Padding = new Thickness(15, 5, 5, 5),
                        };
                        Label SBD = new Label()
                        {
                            Content = "SBD: " + lich.SoBaoDanh,
                            FontSize = 16,
                            Foreground = Brushes.White,
                            Padding = new Thickness(15, 5, 5, 5),
                        };
                        panel.Children.Add(tenMon);
                        panel.Children.Add(thoiGian);
                        panel.Children.Add(hinhThuc);
                        panel.Children.Add(SBD);
                        spnlView.Children.Add(panel);
                    }
                }
            }
            if (soLich == 0)
            {
                StackPanel panel = new StackPanel()
                {
                    Background = Brushes.Olive,
                    Margin = new Thickness(5),
                };
                Label duocNghi = new Label()
                {
                    Content = "Bạn được nghỉ!",
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White,
                    VerticalAlignment = VerticalAlignment.Center,
                    Padding = new Thickness(10, 30, 0, 30)
                };
                panel.Children.Add(duocNghi);
                spnlView.Children.Add(panel);
            }
        }
        private void Panel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        //Lay thang
        private void getMonth(int month, int year)
        {
            if (month < 10)
            {
                lblMonth.Text = "Tháng 0" + month + ", " + year;
            }
            else lblMonth.Text = "Tháng " + month + ", " + year;
        }
        //Lay ngay thang nam
        private void getFullDate()
        {
            var str = lblMonth.Text;
            day = DateTime.Now.Day;
            month = Convert.ToInt32(str.Substring(str.IndexOf(" ") + 1, 2));
            year = Convert.ToInt32(str.Substring(str.LastIndexOf(" ") + 1, 4));
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            getFullDate();
            if (month > 1)
            {
                month -= 1;
            }
            else
            {
                month = 12;
                year -= 1;
            }
            getMonth(month, year);
            loadLich(month, year);
            pushData();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            getFullDate();
            if (month < 12)
            {
                month += 1;
            }
            else
            {
                month = 1;
                year += 1;
            }
            getMonth(month, year);
            loadLich(month, year);
            pushData();
        }
        private void pushData()
        {
            getFullDate();
            var maxDay = DateTime.DaysInMonth(year, month);
            int[] a = new int[maxDay + 1]; //Lưu số lịch của ngày
            for (int i = 0; i < Commons.lstLichHoc.Count; i++)
            {
                Lich lich = Commons.lstLichHoc[i] as Lich;
                var ngay = Convert.ToDateTime(lich.Ngay);
                if (ngay.Month == month && ngay.Year == year)
                {
                    a[ngay.Day]++;
                    if (lich.LoaiLich == "LichThi")
                    {
                        a[ngay.Day] = -1;
                    }
                }
            }
            for (int i = 0; i < gCalender.Children.Count; i++)
            {
                if (gCalender.Children[i] != null)
                {
                    StackPanel stack = (StackPanel)gCalender.Children[i];
                    Label lbl = (Label)stack.Children[0];
                    for (int j = 0; j < a.Length; j++)
                    {
                        if ((int)lbl.Content == j)
                        {
                            Label cal = new Label()
                            {
                                FontSize = 16,
                                FontWeight = FontWeights.Bold,
                                HorizontalContentAlignment = HorizontalAlignment.Center,
                                VerticalContentAlignment = VerticalAlignment.Center,
                                Padding = new Thickness(0)

                            };
                            if (a[j] == 0)
                            {
                                lbl.MinHeight = 50;
                                lbl.Padding = new Thickness(0);
                            }
                            if (a[j] == 1)
                            {
                                cal.Content = "●";
                                cal.Foreground = Brushes.Aqua;
                                stack.Children.Add(cal);
                            }
                            if (a[j] == 2)
                            {
                                cal.Content = "●●";
                                cal.Foreground = Brushes.Blue;
                                stack.Children.Add(cal);
                            }
                            if (a[j] == 3)
                            {
                                cal.Content = "●●●";
                                cal.Foreground = Brushes.Blue;
                                stack.Children.Add(cal);
                            }
                            if (a[j] > 3)
                            {
                                cal.Content = "●●+";
                                cal.Foreground = Brushes.Blue;
                                stack.Children.Add(cal);
                            }
                            if (a[j] == -1)
                            {
                                cal.Content = "❂";
                                cal.Foreground = Brushes.Red;
                                stack.Children.Add(cal);
                            }
                        }
                    }
                }
            }
            getFullDate();
            if (day == DateTime.Now.Day && month == DateTime.Now.Month && year == DateTime.Now.Year)
            {
                MouseButtonEventArgs mouse = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
                LblDay_MouseLeftButtonDown(lblNow, mouse);
            }
        }
        private SolidColorBrush rdColor()
        {
            Random random = new Random();
            int x = random.Next(5);
            if (x == 0) return Brushes.DarkOliveGreen;
            if (x == 1) return Brushes.RoyalBlue;
            if (x == 2) return Brushes.BlueViolet;
            if (x == 3) return Brushes.LightSeaGreen;
            if (x == 4) return Brushes.DarkGreen;
            return null;
        }
    }
}
