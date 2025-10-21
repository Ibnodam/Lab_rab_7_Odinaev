using Sample.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hook.Model;

public partial class Boat : BaseClass
{
    [Key]
    public int BoatId { get; set; }
    private string boatName;
    public string BoatName
    {
        get { return boatName; }
        set { boatName = value; OnPropertyChanged(nameof(BoatName)); }
    }
    private string boatType;
    public string BoatType
    {
        get { return boatType; }
        set { boatType = value; OnPropertyChanged(nameof(BoatType)); }
    }
    private int displacement;
    public int Displacement
    {
        get { return displacement; }
        set
        {
            displacement = value;
            OnPropertyChanged(nameof(Displacement));
        }
    }

    private DateTime buildDate = DateTime.Now;
    public DateTime BuildDate { get { return buildDate; } set { buildDate = value; OnPropertyChanged(nameof(BuildDate)); } }

    public virtual ICollection<Voyage> Voyages { get; set; } = new List<Voyage>();
}
