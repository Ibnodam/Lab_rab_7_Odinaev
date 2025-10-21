using Hook;
using Hook.Model;
using Microsoft.EntityFrameworkCore;
using Sample.Model;
using Sample.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sample.ViewModel
{
    internal class VoyageViewModel : BaseClass
    {
        private Voyage selectedVoyage;
        private HookContext hookContext;
        public HookContext db= new HookContext();
        public Voyage SelectedVoyage
        {
            get { return selectedVoyage; }
            set
            {
                selectedVoyage = value;
                OnPropertyChanged(nameof(SelectedVoyage));
            }
        }
        public ObservableCollection<Voyage> Voyages { get; set; }

        public VoyageViewModel()
        {
            Voyages = new ObservableCollection<Voyage>();
            hookContext = new HookContext();
            List<Voyage> voyageList = hookContext.Voyages.ToList();
            for (int i = 0; i < voyageList.Count; i++)
            {
            //    voyageList[i].BoatName = hookContext.Boats.Where(a => a.BoatId == voyageList[i].BoatId).FirstOrDefault()!.BoatName;
                Voyages.Add(voyageList[i]);
            }
        }
        private RelayCommand removeVoyageCommand;
        public RelayCommand RemoveVoyageCommand
        {
            get
            {
                return removeVoyageCommand ??
                  (removeVoyageCommand = new RelayCommand(obj =>
                  {
                      // Получение выбранного BoatId (предполагаем, что это ComboBox)
                      int selectedVoyageId = (int)selectedVoyage.VoyageId;

                      // Удаление записи из базы данных
                      using (HookContext db = new HookContext())
                      {
                          Voyage voyageToDelete = db.Voyages.FirstOrDefault(v => v.VoyageId == selectedVoyageId);

                          if (voyageToDelete != null)
                          {
                              db.Voyages.Remove(voyageToDelete);
                              db.SaveChanges();
                          }
                      }

                      // Очист                    

                      // Навигация на страницу AddVoyage
                      MainWindow.Instance.PageContainer.Navigate(new BankPage());
                  }));
            }




        }
        private RelayCommand addVoyageCommand;
        public RelayCommand AddVoyageCommand
        {
            get
            {
                return addVoyageCommand ??
                  (addVoyageCommand = new RelayCommand(obj =>
                  {
                      MainWindow.Instance.PageContainer.Navigate(new AddVoyage(new Voyage()));

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
                      Voyage? user = selectedItem as Voyage;
                      if (user == null) return;
                      db.Voyages.Remove(user);
                      db.SaveChanges();
                      Voyages.Remove(user);


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
                           Voyage? user = selectedItem as Voyage;
                           if (user == null) return;

                           // Создаем копию объекта Boat
                           Voyage vy = new Voyage
                           {
                               DepartureDate = user.DepartureDate,
                               BoatId = user.BoatId,
                               ReturnDate = user.ReturnDate,
                               CaptainName = user.CaptainName,

                           };
                         
                      

                           // Открываем окно для редактирования
                           EditVoyageWindow voyageWindow = new EditVoyageWindow(vy);
                           if (voyageWindow.ShowDialog() == true)
                           {
                               user.BoatId = voyageWindow.Voyage.BoatId;
                               user.ReturnDate = voyageWindow.Voyage.ReturnDate;
                               user.CaptainName = voyageWindow.Voyage.CaptainName;
                               user.DepartureDate = voyageWindow.Voyage.DepartureDate;
                               hookContext.Entry(user).State = EntityState.Modified;
                               hookContext.SaveChanges();

                           }
                       }));
            }
        }
    }
}
