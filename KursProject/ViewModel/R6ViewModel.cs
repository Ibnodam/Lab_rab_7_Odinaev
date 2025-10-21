using Hook.Model;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ViewModel
{
   public class R6ViewModel:BaseClass
    {
        private ObservableCollection<R6class> r6Result;
       
        private HookContext db = new HookContext();
        private string spName;
        public string SpName
        {
            get { return spName; }
            set
            {
                spName = value;
                OnPropertyChanged(nameof(SpName));
            }
        }
        private string fshName;
        public string FshName
        {
            get { return fshName; }
            set
            {
                fshName = value;
                OnPropertyChanged(nameof(FshName));

            }
        }
        public class R6class
        {
            public string? fshName { get; set; }
            public string? spName { get; set; }
            public DateTime dpDate { get; set; }
            public int cWeight { get; set; }
            public string bName { get; set; }
        }

       
        public ObservableCollection<R6class>Filtred
        {
            get
            {
                return r6Result;
            }
            set
            {
                r6Result= value;
                OnPropertyChanged(nameof(Filtred));
            }
        }
        private RelayCommand? fitreCommand;
        public RelayCommand FitreCommand
        {
            get
            {
                return fitreCommand ??
               (fitreCommand = new RelayCommand(obj =>
               {
               if (SpName != null && fshName != null)
                   {
                       List<R6class> query = (from c in db.Catches
                                              join v in db.SpotVisits on c.VisitId equals v.VisitId
                                              join fs in db.FishingSpots on v.SpotId equals fs.SpotId
                                              join voy in db.Voyages on v.VoyageId equals voy.VoyageId
                                              join bt in db.Boats on voy.BoatId equals bt.BoatId
                                              where (fs.SpotName==spName&&c.FishType==fshName)
                                             select new R6class
                                              { cWeight=c.CatchWeight, 
                                                 fshName=c.FishType,
                                                  dpDate= voy.DepartureDate,
                                                   spName= fs.SpotName,
                                                  bName=bt.BoatName })
                                              .ToList();



                       Filtred = new ObservableCollection<R6class>(query);


                   }

               })
              );
            }
        }
    }
}
