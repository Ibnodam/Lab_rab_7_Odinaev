using Hook;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sample.View
{
    /// <summary>
    /// Логика взаимодействия для AddCatch.xaml
    /// </summary>
    public partial class AddCatch : Page
    {
        public AddCatch(Catch _catch)
        {
            InitializeComponent();
            HookContext db = new HookContext();
            List<SpotVisit> NSpots = db.SpotVisits.ToList();
            List<string>types = new List<string> {"Лосось","Тунец", "Треска", "Палтус","Форель", "Морской окунь", "Скумбрия", "Анчоусы", "Тилапия", "Карась"}.ToList();
            VisitIdCB.ItemsSource = NSpots;
           typefish.ItemsSource =types ;

            ThisCatch = _catch;
            DataContext = ThisCatch;
        }
        public Catch ThisCatch { get; private set; }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {using (HookContext db = new HookContext())
            {
                db.Catches.Add(ThisCatch);
                await db.SaveChangesAsync();
            }
           MainWindow.Instance.PageContainer.Navigate(new CatchWindow());

        }
    }
}
