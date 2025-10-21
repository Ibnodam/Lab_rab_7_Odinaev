using Hook.Model;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ViewModel
{
   public class R5ViewModel:BaseClass
    {
        private HookContext db = new HookContext();

        public ObservableCollection<Boat> Boats { get; set; }
        public ObservableCollection<Voyage> voyages { get; set; }
        public ObservableCollection<SpotVisit> visits { get; set; }

        public ObservableCollection<Req1> Request1 { get; set; }
        public R5ViewModel()
        {
            Boats = new ObservableCollection<Boat>();
            voyages = new ObservableCollection<Voyage>();
            visits = new ObservableCollection<SpotVisit>();
            List<Boat> boatlist = db.Boats.ToList();
            List<Voyage> voyagelist = db.Voyages.ToList();
            List<SpotVisit> visitlist = db.SpotVisits.ToList();
            for (int i = 0; i < boatlist.Count; i++)
            {
                Boats.Add(boatlist[i]);
            }
            for (int i = 0; i < voyagelist.Count; i++)
            {
                voyages.Add(voyagelist[i]);
            }
            for (int i = 0; i < visitlist.Count; i++)
            {
                visits.Add(visitlist[i]);
            }

            List<Req1> result = (from c in db.Catches
                     join v in db.SpotVisits on c.VisitId equals v.VisitId
                     join fs in db.FishingSpots on v.SpotId equals fs.SpotId
                     join voy in db.Voyages on v.VoyageId equals voy.VoyageId
                     join bt in db.Boats on voy.BoatId equals bt.BoatId
                     select new { c.FishType, voy.DepartureDate, c.CatchWeight, voy.ReturnDate })
                     .GroupBy(x => x.FishType)
                     .Select(g => new Req1
                     {
                         Ftype = g.Key,
                         DepDate = g.FirstOrDefault().DepartureDate,
                         CatchQuality = g.FirstOrDefault().CatchWeight,
                         RetDate = g.FirstOrDefault().ReturnDate
                     })
                     .ToList();

            Request1 = new ObservableCollection<Req1>(result);
        }

        public class Req1
        {
            public string Ftype { get; set; }
            public DateTime DepDate { get; set; }
            public DateTime RetDate { get; set; }
            public int CatchQuality { get; set; }

        }

    }
}
