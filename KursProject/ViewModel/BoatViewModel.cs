using Hook;
using Hook.Model;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Sample.Model;
using Sample.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Sample.ViewModel
{
    internal class BoatViewModel : BaseClass
    {
        private Boat selectedBoat;
        private HookContext hookContext;
        private HookContext db = new HookContext();


        public Boat SelectedBoat
        {
            get { return selectedBoat; }
            set
            {
                selectedBoat = value;
                OnPropertyChanged(nameof(selectedBoat));
            }
        }
        public ObservableCollection<Boat> Boats { get; set; }
        public BoatViewModel()
        {
            Boats = new ObservableCollection<Boat>();
            hookContext = new HookContext();
            List<Boat> boatList = hookContext.Boats.ToList();
            for (int i = 0; i < boatList.Count; i++)
            {

                Boats.Add(boatList[i]);
            }
        }
        private RelayCommand addBoatCommand;
        public RelayCommand AddBoatCommand
        {
            get
            {
                return addBoatCommand ??
                  (addBoatCommand = new RelayCommand(obj =>
                  {
                      MainWindow.Instance.PageContainer.Navigate(new AddBoat2(new Boat()));

                  }));
            }
        }
        private RelayCommand deleteBoatCommand;
        public RelayCommand DaleteBoatCommand
        {
            get
            {
                return deleteBoatCommand ??
                  (deleteBoatCommand = new RelayCommand(async obj =>
                 {
                     if (selectedBoat != null)
                     {

                         using (HookContext db = new HookContext())
                         {

                             db.Boats.Remove(selectedBoat);
                             await db.SaveChangesAsync();
                             Boats = new ObservableCollection<Boat>();
                             List<Boat> boatList = db.Boats.ToList();
                         };
                     }

                     MessageBox.Show("Выбранный элемент удален");
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
                      Boat? user = selectedItem as Boat;
                      if (user == null) return;
                      db.Boats.Remove(user);
                      db.SaveChanges();
                      Boats.Remove(user);
                     

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
                           Boat? user = selectedItem as Boat;
                           if (user == null) return;

                           // Создаем копию объекта Boat
                           Boat bt = new Boat
                           {
                               BoatName = user.BoatName,
                               BoatId = user.BoatId,
                               BoatType = user.BoatType,
                               BuildDate = user.BuildDate,
                               Displacement = user.Displacement,
                           };

                           // Открываем окно для редактирования
                           EditBoatWindow boatWindow = new EditBoatWindow(bt);
                           if (boatWindow.ShowDialog() == true) 
                           {
                               user.BoatName = boatWindow.Boat.BoatName;
                               user.BoatType = boatWindow.Boat.BoatType;
                               user.BuildDate = boatWindow.Boat.BuildDate;
                               user.Displacement = boatWindow.Boat.Displacement;
                               hookContext.Entry(user).State = EntityState.Modified;
                               hookContext.SaveChanges();
                              
                           }
                       }));
            }
        }

    }
}
 