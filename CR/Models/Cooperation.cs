using System;

namespace CR.Models;

public class Cooperation
{
    public int ID { get; set; }
    public int Teacher { get; set; }
    public int Parent { get; set; }
    public string Form { get; set; }
    public DateTimeOffset StartDate { get; set; }
}