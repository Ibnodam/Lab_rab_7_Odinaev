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
    internal class CatchViewModel:BaseClass
    {public HookContext db= new HookContext();
        private Catch selectedCatch;
        private HookContext hookContext;
        public Catch SelectedCatch
        {
            get { return selectedCatch; }
            set
            {
                selectedCatch = value;
                OnPropertyChanged(nameof(selectedCatch));
            }
        }
        public ObservableCollection<Catch> Catchs { get; set; }
        public CatchViewModel()
        {
            Catchs = new ObservableCollection<Catch>();
            hookContext = new HookContext();
            List<Catch> catchList = hookContext.Catches.ToList();
            for (int i = 0; i < catchList.Count; i++)
            {

                Catchs.Add(catchList[i]);
            }
        }
        private RelayCommand addCathcCommand;
        public RelayCommand AddCathcCommand
        {
            get
            {
                return addCathcCommand ??
                  (addCathcCommand = new RelayCommand(obj =>
                  {
                      MainWindow.Instance.PageContainer.Navigate(new AddCatch(new Catch()));

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
                      Catch? user = selectedItem as Catch;
                      if (user == null) return;
                      db.Catches.Remove(user);
                      db.SaveChanges();
                      Catchs.Remove(user);


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
                           Catch? user = selectedItem as Catch;
                           if (user == null) return;

                           // Создаем копию объекта Boat
                           Catch ct = new Catch
                           {
                               VisitId = user.VisitId,
                               FishType = user.FishType,
                               CatchWeight = user.CatchWeight,
                              
                           };

                           // Открываем окно для редактирования
                           EditCatchWindow catchWindow = new EditCatchWindow(ct);
                           if (catchWindow.ShowDialog() == true)
                           {
                               user.VisitId = catchWindow.Catch.VisitId;
                               user.FishType = catchWindow.Catch.FishType;
                               user.CatchWeight =catchWindow.Catch.CatchWeight ;
                               hookContext.Entry(user).State = EntityState.Modified;
                               hookContext.SaveChanges();

                           }
                       }));
            }
        }
    }
}
