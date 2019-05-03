using StudentSocial.Common;
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
    /// Interaction logic for PMark.xaml
    /// </summary>
    public partial class PMark : Page
    {
        public PMark()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            loadChung();
            //lvMark.ItemsSource = Commons.lstDiem;
            //loadMark();
            rdAll.IsChecked = true;
        }

        private void loadChung()
        {
            lblTongTC.Content = Commons.chung.TongTC;
            lblTCKhongDat.Content = Commons.chung.SoTCKhongDat;
            lblSTCTLN.Content = Commons.chung.STCTLN;
            lblSTCTD.Content = Commons.chung.STCTD;
            lblMonKhongDat.Content = Commons.chung.SoMonKhongDat;
            lblDTBCQD.Content = Commons.chung.DTBCQD;
            lblDTBC.Content = Commons.chung.DTBC;
        }

        private void loadMark()
        {
            spnlView.Children.Clear();
            for (int i = 0; i < Commons.lstDiem.Count; i++)
            {
                var diem = Commons.lstDiem[i];
                Grid grid = new Grid()
                {
                    Background = Brushes.LightSkyBlue,
                    Margin = new Thickness(2)
                };
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                Label tenMon = new Label()
                {
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.Bold,
                    Padding = new Thickness(15,5,5,5)
                };
                for (int j = 0; j < Commons.lstMonDiem.Count; j++)
                {
                    if (diem.MaMon == Commons.lstMonDiem[i].MaMon)
                    {
                        tenMon.Content = Commons.lstMonDiem[i].TenMon;
                    }
                }
                Grid.SetRow(tenMon, 0);
                Grid.SetColumn(tenMon, 0);
                Grid.SetColumnSpan(tenMon, 4);
                grid.Children.Add(tenMon);

                //Titile poin
                Label CC = new Label()
                {
                    Content = "CC",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(CC, 1);
                Grid.SetColumn(CC, 0);
                grid.Children.Add(CC);
                Label THI = new Label()
                {
                    Content = "THI",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(THI, 1);
                Grid.SetColumn(THI, 1);
                grid.Children.Add(THI);
                Label TKHP = new Label()
                {
                    Content = "TKHP",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(TKHP, 1);
                Grid.SetColumn(TKHP, 2);
                grid.Children.Add(TKHP);
                Label XL = new Label()
                {
                    Content = "XL",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(XL, 1);
                Grid.SetColumn(XL, 3);
                grid.Children.Add(XL);
                //Content poin
                Label mCC = new Label()
                {
                    Content = diem.CC,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(mCC, 2);
                Grid.SetColumn(mCC, 0);
                grid.Children.Add(mCC);

                Label mTHI = new Label()
                {
                    Content = diem.THI,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(mTHI, 2);
                Grid.SetColumn(mTHI, 1);
                grid.Children.Add(mTHI);

                Label mTKHP = new Label()
                {
                    Content = diem.TKHP,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(mTKHP, 2);
                Grid.SetColumn(mTKHP, 2);
                grid.Children.Add(mTKHP);

                Label mXL = new Label()
                {
                    Content = diem.DiemChu,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(mXL, 2);
                Grid.SetColumn(mXL, 3);
                if (diem.DiemChu == "A") { mXL.Foreground = Brushes.Blue; }
                if (diem.DiemChu == "B") { mXL.Foreground = Brushes.MidnightBlue; }
                if (diem.DiemChu == "C") { mXL.Foreground = Brushes.BlueViolet; }
                if (diem.DiemChu == "D") { mXL.Foreground = Brushes.Black; }
                if (diem.DiemChu == "F") { mXL.Foreground = Brushes.Red; }
                grid.Children.Add(mXL);
                //ADD ALL
                spnlView.Children.Add(grid);
            }
        }
        private void loadMark(String load)
        {
            spnlView.Children.Clear();
            for (int i = 0; i < Commons.lstDiem.Count; i++)
            {
                var diem = Commons.lstDiem[i];
                if (diem.DiemChu == load)
                {
                    Grid grid = new Grid()
                    {
                        Background = Brushes.LightSkyBlue,
                        Margin = new Thickness(2)
                    };
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    Label tenMon = new Label()
                    {
                        Foreground = Brushes.Black,
                        FontWeight = FontWeights.Bold,
                        Padding = new Thickness(15, 5, 5, 5)
                    };
                    for (int j = 0; j < Commons.lstMonDiem.Count; j++)
                    {
                        if (diem.MaMon == Commons.lstMonDiem[i].MaMon)
                        {
                            tenMon.Content = Commons.lstMonDiem[i].TenMon;
                        }
                    }
                    Grid.SetRow(tenMon, 0);
                    Grid.SetColumn(tenMon, 0);
                    Grid.SetColumnSpan(tenMon, 4);
                    grid.Children.Add(tenMon);

                    //Titile poin
                    Label CC = new Label()
                    {
                        Content = "CC",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(CC, 1);
                    Grid.SetColumn(CC, 0);
                    grid.Children.Add(CC);
                    Label THI = new Label()
                    {
                        Content = "THI",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(THI, 1);
                    Grid.SetColumn(THI, 1);
                    grid.Children.Add(THI);
                    Label TKHP = new Label()
                    {
                        Content = "TKHP",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(TKHP, 1);
                    Grid.SetColumn(TKHP, 2);
                    grid.Children.Add(TKHP);
                    Label XL = new Label()
                    {
                        Content = "XL",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(XL, 1);
                    Grid.SetColumn(XL, 3);
                    grid.Children.Add(XL);
                    //Content poin
                    Label mCC = new Label()
                    {
                        Content = diem.CC,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(mCC, 2);
                    Grid.SetColumn(mCC, 0);
                    grid.Children.Add(mCC);

                    Label mTHI = new Label()
                    {
                        Content = diem.THI,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(mTHI, 2);
                    Grid.SetColumn(mTHI, 1);
                    grid.Children.Add(mTHI);

                    Label mTKHP = new Label()
                    {
                        Content = diem.TKHP,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(mTKHP, 2);
                    Grid.SetColumn(mTKHP, 2);
                    grid.Children.Add(mTKHP);

                    Label mXL = new Label()
                    {
                        Content = diem.DiemChu,
                        FontWeight = FontWeights.Bold,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(mXL, 2);
                    Grid.SetColumn(mXL, 3);
                    if (diem.DiemChu == "A") { mXL.Foreground = Brushes.Blue; }
                    if (diem.DiemChu == "B") { mXL.Foreground = Brushes.MidnightBlue; }
                    if (diem.DiemChu == "C") { mXL.Foreground = Brushes.BlueViolet; }
                    if (diem.DiemChu == "D") { mXL.Foreground = Brushes.Black; }
                    if (diem.DiemChu == "F") { mXL.Foreground = Brushes.Red; }
                    grid.Children.Add(mXL);
                    //ADD ALL
                    spnlView.Children.Add(grid);
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Tag.ToString() == "ALL")
            {
                loadMark();
            }
            if (radio.Tag.ToString() == "A")
            {
                loadMark("A");
            }
            if (radio.Tag.ToString() == "B")
            {
                loadMark("B");
            }
            if (radio.Tag.ToString() == "C")
            {
                loadMark("C");
            }
            if (radio.Tag.ToString() == "D")
            {
                loadMark("D");
            }
            if (radio.Tag.ToString() == "F")
            {
                loadMark("F");
            }
        }
    }
}
