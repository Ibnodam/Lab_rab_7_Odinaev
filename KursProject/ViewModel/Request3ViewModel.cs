using Hook.Model;
using Sample.Model;
using Sample.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Sample.ViewModel.R4ViewModel;
using static Sample.ViewModel.Request2ViewModel;

namespace Sample.ViewModel
{
    public class Request3ViewModel : BaseClass
    {
    
        private HookContext db = new HookContext();
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
            public double AvgCatch { get; set; } // Средний улов
            public string Name { get; set; }     // Имя места ловли (SpotName)
                                                 //Type поле можно убрать, оно здесь не нужно.
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
                                              select new { c.CatchWeight, fs.SpotName })
                    .GroupBy(x => x.SpotName)
                    .Select(g => new R2class
                    {
                        AvgCatch =Math.Round(g.DefaultIfEmpty().Average(x => x.CatchWeight),2), // Среднее значение, обработка пустых групп
                        Name = g.Key // Имя места ловли (SpotName)
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
