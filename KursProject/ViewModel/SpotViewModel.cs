using Hook;
using Hook.Model;
using Microsoft.EntityFrameworkCore;
using Sample.Model;
using Sample.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sample.ViewModel
{
    internal class SpotViewModel:BaseClass
    {
        private FishingSpot selectedSpot;
        private HookContext hookContext;
        private HookContext db= new HookContext();
        public FishingSpot SelectedSpot
        {
            get { return selectedSpot; }
            set
            {
                selectedSpot = value;
                OnPropertyChanged(nameof(selectedSpot));
            }
        }
        public ObservableCollection<FishingSpot> Spots { get; set; }
        public SpotViewModel()
        {
            Spots = new ObservableCollection<FishingSpot>();
            hookContext = new HookContext();
            List<FishingSpot> spotList = hookContext.FishingSpots.ToList();
            for (int i = 0; i < spotList.Count; i++)
            {

                Spots.Add(spotList[i]);
            }
        }

        private RelayCommand addSpotCommand;
        public RelayCommand AddSpotCommand
        {
            get
            {
                return addSpotCommand ??
                  (addSpotCommand = new RelayCommand(obj =>
                  {
                      MainWindow.Instance.PageContainer.Navigate(new AddSpotPage(new FishingSpot()));

                  }));
            }
        }
        private RelayCommand? deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      FishingSpot? user = selectedItem as FishingSpot;
                      if (user == null) return;
                      db.FishingSpots.Remove(user);
                      db.SaveChanges();
                      Spots.Remove(user);


                  }));
            }
        }
        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                       (editCommand = new RelayCommand((selectedItem) =>
                       {
                           FishingSpot? user = selectedItem as FishingSpot;
                           if (user == null) return;

                           // Создаем копию объекта Boat
                           FishingSpot fs = new FishingSpot
                           {
                               SpotId= user.SpotId,
                              SpotName= user.SpotName,
                             
                           };

                           // Открываем окно для редактирования
                           EditSpotWindow spotWindow = new EditSpotWindow(fs);
                           if (spotWindow.ShowDialog() == true)
                           {
                               user.SpotName = spotWindow.FishingSpot.SpotName;
                               hookContext.Entry(user).State = EntityState.Modified;
                               hookContext.SaveChanges();

                           }
                       }));
            }
        }
    }
}
