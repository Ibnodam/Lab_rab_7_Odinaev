using Hook;
using Hook.Model;
using Sample.Model;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Логика взаимодействия для Addvisit.xaml
    /// </summary>
    public partial class Addvisit : Page
    {
        public Addvisit(SpotVisit _visit)
        {
            InitializeComponent();
            HookContext db= new HookContext();
            List<FishingSpot>NSpots= db.FishingSpots.ToList();
            NameBank.ItemsSource=NSpots;
            List<Voyage> VoyagId = db.Voyages.ToList();
            IdVoyage.ItemsSource=VoyagId;
            ThisVisit=_visit;
            DataContext = ThisVisit;
        }
        public SpotVisit ThisVisit { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                using (HookContext db = new HookContext())
                {
                db.SpotVisits.Add(ThisVisit);
                db.SaveChangesAsync();
            }
            MainWindow.Instance.PageContainer.Navigate(new VisitWindow());





        }
    }
}
