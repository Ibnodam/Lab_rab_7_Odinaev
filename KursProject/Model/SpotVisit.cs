using Sample.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hook.Model;

public partial class SpotVisit : BaseClass
{
    [Key]
    public int VisitId { get; set; }
    private int spotId;
    public int SpotId
    {
        get { return spotId; }
        set { spotId = value; OnPropertyChanged(nameof(SpotId)); }
    }
    private int voyageId;
    public int VoyageId
    {
        get { return voyageId; }
        set { voyageId = value; OnPropertyChanged(nameof(VoyageId)); }
    }
    private DateTime arrivalDate = DateTime.Now;
    public DateTime ArrivalDate
    {
        get { return arrivalDate; }
        set { arrivalDate = value; OnPropertyChanged(nameof(ArrivalDate)); }
    }

    private DateTime departureDate = DateTime.Now;
    public DateTime DepartureDate { get { return departureDate; } set { departureDate = value; OnPropertyChanged(nameof(DepartureDate)); } }
    private string catchQuality;
    public string CatchQuality
    {
        get { return catchQuality; }
        set { catchQuality = value; OnPropertyChanged(nameof(CatchQuality)); }
    }

    public virtual ICollection<Catch> Catches { get; set; } = new List<Catch>();

    public virtual FishingSpot Spot { get; set; } = null!;

    public virtual Voyage Voyage { get; set; } = null!;
}
