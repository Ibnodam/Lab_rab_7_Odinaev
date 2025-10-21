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
    /// Логика взаимодействия для EditCatchWindow.xaml
    /// </summary>
    public partial class EditCatchWindow : Window
    {public Catch Catch { get; private set; }
        public EditCatchWindow( Catch _catch)
        {
            InitializeComponent();
            Catch = _catch;
            DataContext = Catch;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult= true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
