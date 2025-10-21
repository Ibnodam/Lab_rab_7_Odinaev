using Hook;
using Hook.Model;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
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

namespace Sample.View
{
    /// <summary>
    /// Логика взаимодействия для AddVoyage.xaml
    /// </summary>
    public partial class AddVoyage : Page

    {
        public AddVoyage(Voyage _voyage)
        {
            InitializeComponent();
            HookContext db = new HookContext();
            List<Boat> boatNamelist = db.Boats.ToList();
            NameKater.ItemsSource = boatNamelist;
            ThisVoyage = _voyage;
            DataContext = ThisVoyage;
           
        }
       
        public Voyage ThisVoyage { get; private set; } 
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HookContext db = new HookContext())
            {
               
                db.Voyages.Add(ThisVoyage);
                await db.SaveChangesAsync();
            }

            MainWindow.Instance.PageContainer.Navigate(new BankPage());

        }
      
        }
    
}
