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
using System.Windows;
using System.Windows.Media;

namespace Sample.ViewModel
{
    internal class VisitViewModel : BaseClass
    {
        private SpotVisit selectedVisit;
        private HookContext hookContext;
        private HookContext db = new HookContext();
        public SpotVisit SelectedVisit
        {
            get { return selectedVisit; }
            set
            {
                selectedVisit = value;
                OnPropertyChanged(nameof(selectedVisit));
            }
        }
        public ObservableCollection<SpotVisit> Visits { get; set; }
        public VisitViewModel()
        {
            Visits = new ObservableCollection<SpotVisit>();
            hookContext = new HookContext();
            List<SpotVisit> visitlist = hookContext.SpotVisits.ToList();
            for (int i = 0; i < visitlist.Count; i++)
            {
                Visits.Add(visitlist[i]);
            }
        }
        private RelayCommand addVisitCommand;
        public RelayCommand AddVisitCommand
        {
            get
            {
                return addVisitCommand ??
                  (addVisitCommand = new RelayCommand(obj =>
                  {
                      MainWindow.Instance.PageContainer.Navigate(new Addvisit(new SpotVisit()));

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
                      SpotVisit? user = selectedItem as SpotVisit;
                      if (user == null) return;
                      db.SpotVisits.Remove(user);
                      db.SaveChanges();
                      Visits.Remove(user);


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
                           SpotVisit? user = selectedItem as SpotVisit;
                           if (user == null) return;

                           // Создаем копию объекта Boat
                           SpotVisit sv = new SpotVisit
                           {
                               DepartureDate = user.DepartureDate,
                               VisitId = user.VisitId,
                               ArrivalDate = user.ArrivalDate,
                               CatchQuality = user.CatchQuality,
                               SpotId = user.SpotId,
                               VoyageId = user.VoyageId,

                           };



                           // Открываем окно для редактирования
                           EditVisitWindow visitWindow = new EditVisitWindow(sv);
                           if (visitWindow.ShowDialog() == true)
                           {
                               user.VisitId = visitWindow.SpotVisit.VisitId;
                               user.VoyageId = visitWindow.SpotVisit.VoyageId;
                               user.SpotId = visitWindow.SpotVisit.SpotId;
                               user.ArrivalDate = visitWindow.SpotVisit.ArrivalDate;
                               user.DepartureDate = visitWindow.SpotVisit.DepartureDate;
                               user.CatchQuality = visitWindow.SpotVisit.CatchQuality;
                               hookContext.Entry(user).State = EntityState.Modified;
                               hookContext.SaveChanges();

                           }
                       }));
            }
        }
    }
}
