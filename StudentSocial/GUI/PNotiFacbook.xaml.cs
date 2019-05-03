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

namespace StudentSocial.GUI
{
    /// <summary>
    /// Interaction logic for PNotiFacbook.xaml
    /// </summary>
    public partial class PNotiFacbook : Page
    {
        public PNotiFacbook()
        {
            InitializeComponent();
        }

        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtLoginToken.Text);
            lblStatus.Visibility = Visibility.Visible;
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.messenger.com/t/2191507134472233");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtLoginToken.Text = "login:"+File.ReadAllText(Paths.token)+":"+File.ReadAllText(Paths.hocky);
        }
    }
}
