using System;

namespace CR.Models;

public class CooperationModel
{
    public int ID { get; set; }
    public string Teacher { get; set; }
    public string Parent { get; set; }
    public string Form { get; set; }
    public DateTimeOffset StartDate { get; set; }
}