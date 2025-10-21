using Sample.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hook.Model;

public partial class Voyage : BaseClass
{
    [Key]
    public int VoyageId { get; set; }

    private int boatId;
    public int BoatId
    {
        get { return boatId; }
        set
        {
            boatId = value;
            OnPropertyChanged(nameof(BoatId));
        }
    }
    private DateTime departureDate = DateTime.Now;
    public DateTime DepartureDate
    {
        get { return departureDate; }
        set
        {
            departureDate = value; OnPropertyChanged(nameof(DepartureDate));
        }

    }
    private DateTime returnDate = DateTime.Now;
    public DateTime ReturnDate
    {
        get { return returnDate; }
        set { returnDate = value; OnPropertyChanged(nameof(ReturnDate)); }
    }

    private string captainName;
    public string CaptainName { get { return captainName; } set { captainName = value; OnPropertyChanged(nameof(CaptainName)); } }

    public virtual Boat Boat { get; set; }

    public virtual ICollection<SpotVisit> SpotVisits { get; set; } = new List<SpotVisit>();
}
