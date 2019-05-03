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
using System.Windows.Shapes;

namespace StudentSocial.GUI
{
    /// <summary>
    /// Interaction logic for WNotes.xaml
    /// </summary>
    public partial class WNotes : Window
    {
        public WNotes()
        {
            InitializeComponent();
        }
        DateTime date;
        public WNotes(int ngay, int thang, int nam)
        {
            InitializeComponent();
            this.date = new DateTime(nam, thang, ngay);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this.date.ToLongDateString());
        }
    }
}
