using Hook.Model;
using Sample.Model;
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

namespace Sample.View
{
    /// <summary>
    /// Логика взаимодействия для EditVisitWindow.xaml
    /// </summary>
    public partial class EditVisitWindow : Window
    {public SpotVisit SpotVisit { get; set; }
        public EditVisitWindow(SpotVisit _spotVisit)
        {
            InitializeComponent();
            SpotVisit = _spotVisit;
            DataContext = SpotVisit;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult=false;
        }
    }
}
