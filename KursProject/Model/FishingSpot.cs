using Sample.Model;
using System;
using System.Collections.Generic;

namespace Hook.Model;

public partial class FishingSpot : BaseClass
{
    private int spotId;
    public int SpotId
    {
        get { return spotId; }
        set
        {
            spotId = value; OnPropertyChanged(nameof(SpotId));
        }
    }
    private string spotName;
    public string SpotName
    {
        get { return spotName; }
        set { spotName = value; OnPropertyChanged(nameof(SpotName)); }
    }

    public virtual ICollection<SpotVisit> SpotVisits { get; set; } = new List<SpotVisit>();
}
