using Sample.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hook.Model;

public partial class Catch : BaseClass
{
    [Key]
    public int CatchId { get; set; }
    private int visitId;
    public int VisitId
    {
        get { return visitId; }
        set { visitId = value; OnPropertyChanged(nameof(VisitId)); }
    }
    private string fishType;
    public string FishType
    {
        get { return fishType; }
        set { fishType = value; OnPropertyChanged(nameof(FishType)); }
    }
    private int catchWeight;
    public int CatchWeight
    {
        get { return catchWeight; }
        set { catchWeight = value; OnPropertyChanged(nameof(CatchWeight)); }
    }

    public virtual SpotVisit Visit { get; set; } = null!;
}