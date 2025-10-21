using Hook.Model;
using Sample.Model;
using Sample.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Markup;

namespace Sample.ViewModel
{
   public class Request2ViewModel:BaseClass
    {
       
        private HookContext db= new HookContext();
        private ObservableCollection<R2class>? filteredBoats;
        private DateTime? startDate;
        public DateTime? StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        //public Request2ViewModel(R2Window window)
        //{
        //    this.context = window;
        //}

        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public ObservableCollection<R2class> FilteredBoats
        {
            get { return filteredBoats; }
            set
            {
                filteredBoats = value;
                OnPropertyChanged(nameof(FilteredBoats));
            }
        }
        public class R2class
        {
            public string Name { get; set; }
             public string Type { get; set; }
            public int Maxcatch { get; set; }

        }

        private RelayCommand? fitreCommand;
        public RelayCommand FitreCommand
        {
            get
            {
                return fitreCommand ??
               (fitreCommand = new RelayCommand(obj =>
               {
                   if (StartDate.HasValue && EndDate.HasValue)
                   {
                       List<R2class> query = (from c in db.Catches
                                              join v in db.SpotVisits on c.VisitId equals v.VisitId
                                              join fs in db.FishingSpots on v.SpotId equals fs.SpotId
                                              join voy in db.Voyages on v.VoyageId equals voy.VoyageId
                                              join bt in db.Boats on voy.BoatId equals bt.BoatId
                                              where (voy.DepartureDate >= StartDate && voy.DepartureDate <= EndDate)
                                              select new { c.CatchWeight, bt.BoatName, c.FishType, c.CatchId }) // Изменен select
                                              .GroupBy(x => x.FishType) // Группировка по типу рыбы
                                              .Select(g => new R2class // Выбираем максимум для каждой группы
                                              {
                                                  Maxcatch = g.Max(x => x.CatchWeight),
                                                  Name = g.OrderByDescending(x => x.CatchWeight).First().BoatName, //получаем имя лодки с максимальным уловом
                                                  Type = g.Key
                                              })
                                              .ToList();


                     
                       FilteredBoats = new ObservableCollection<R2class>(query);
                       

                   }

               })
              );
            }
        }
    }
}
