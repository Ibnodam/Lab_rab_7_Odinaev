using Hook.Model;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sample.ViewModel
{
    public class R1ViewModel : BaseClass
    {
        private HookContext db = new HookContext();

        public ObservableCollection<Boat> Boats { get; set; }
        public ObservableCollection<Voyage> voyages { get; set; }
        public ObservableCollection<SpotVisit> visits { get; set; }

        public ObservableCollection<Req1> Request1 { get; set; }
        public R1ViewModel() 
        {
            Boats = new ObservableCollection<Boat>();
            voyages = new ObservableCollection<Voyage>();
            visits = new ObservableCollection<SpotVisit>();
            List<Boat> boatlist = db.Boats.ToList();
            List<Voyage> voyagelist = db.Voyages.ToList();
            List<SpotVisit> visitlist = db.SpotVisits.ToList();
            for (int i =0; i<boatlist.Count; i++)
            {
                Boats.Add(boatlist[i]);
            }
            for(int i =0; i<voyagelist.Count; i++)
            {
                voyages.Add(voyagelist[i]);
            }
            for(int i =0; i<visitlist.Count; i++)
            {
                visits.Add(visitlist[i]);
            }

            List<Req1> result = (from boat in Boats
                          join voyage in voyages on boat.BoatId equals voyage.BoatId
                          join visit in visits on voyage.VoyageId equals visit.VoyageId
                          select new Req1
                          {
                              Boatname = boat.BoatName,
                              DepDate = voyage.DepartureDate,
                              CatchQuality = visit.CatchQuality
                          }).ToList();

            Request1 = new ObservableCollection<Req1>(result);
        }
       
        public class Req1
        {
            public string Boatname { get; set; }
            public DateTime DepDate { get; set; }
            public string CatchQuality { get; set; }

        }


       
        
           
        
    }
}