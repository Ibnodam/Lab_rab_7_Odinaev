using Hook;
using Hook.Model;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace Sample.View
{
    /// <summary>
    /// Логика взаимодействия для AddBoat.xaml
    /// </summary>
    public partial class AddBoat2 : Page
    {
        public AddBoat2(Boat _boat)
        {
            InitializeComponent();

            ThisBoat = _boat;
            DataContext = ThisBoat;
        }
        
        public Boat ThisBoat { get; private set; }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            using (HookContext db = new HookContext())
            {
                db.Boats.Add(ThisBoat);
                await db.SaveChangesAsync();
            }
            MainWindow.Instance.PageContainer.Navigate(new BoatWindow());
        }

   
    }
}

