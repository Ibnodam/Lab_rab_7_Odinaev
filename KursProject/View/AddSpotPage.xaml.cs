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
    /// Логика взаимодействия для AddSpotPage.xaml
    /// </summary>
    public partial class AddSpotPage : Page
    {
        public AddSpotPage(FishingSpot _spot)

        {ThisSpot = _spot;
            DataContext = ThisSpot;
            InitializeComponent();
        }
        public FishingSpot ThisSpot { get; private set; }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HookContext db = new HookContext())
            {
                db.FishingSpots.Add(ThisSpot);
                await db.SaveChangesAsync();
            }
            MainWindow.Instance.PageContainer.Navigate(new SpotWindow());


        }

    }
}
