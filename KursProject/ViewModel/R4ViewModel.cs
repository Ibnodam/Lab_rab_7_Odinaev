using Hook.Model;
using Microsoft.EntityFrameworkCore;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sample.ViewModel
{
    public class R4ViewModel : BaseClass
    {
        private HookContext db = new HookContext(); // Создаем db здесь
        private ObservableCollection<R4Class> rResults;
        public ObservableCollection<R4Class> RResults 
        {
            get {return rResults; }
            set { rResults = value; OnPropertyChanged(nameof(RResults)); }
        }

        //public class R4Results
        //  {
        //      public string BoatName;
        //      public int CatchWeight;
        //  }


        public class R4Class
        {
            public string BName { get; set; }
            public int cWeight { get; set; }

        }

        private string _selectedSpot; // Используем _selectedSpot
        public string SelectedSpot
        {
            get { return _selectedSpot; }
            set { _selectedSpot = value; OnPropertyChanged(nameof(SelectedSpot)); }
        }

        private RelayCommand? _filtredSpotCommand;
        public RelayCommand FilterSpotCommand
        {
            get
            {
                return _filtredSpotCommand ?? 
               (_filtredSpotCommand = new RelayCommand(obj =>
                {
                    if (!string.IsNullOrEmpty(SelectedSpot))
                    {
                        var Aver = (from c in db.Catches
                                    join v in db.SpotVisits on c.VisitId equals v.VisitId
                                    join fs in db.FishingSpots on v.SpotId equals fs.SpotId
                                    join voy in db.Voyages on v.VoyageId equals voy.VoyageId
                                    join bt in db.Boats on voy.BoatId equals bt.BoatId
                                    where (fs.SpotName== SelectedSpot) && 
                                    (c.CatchWeight>db.Catches.Average(p=>p.CatchWeight))
                                    select new R4Class
                                    {
                                        BName=bt.BoatName,    
                                     cWeight= c.CatchWeight
                                    }).ToList();
                        RResults = new ObservableCollection<R4Class>(Aver);
                    }
                }));
            }
        }
    }
}
