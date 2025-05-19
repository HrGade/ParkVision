namespace ParkVision.Server.Model;

public class ParkeringDTO
{
    public required int ParkeringID { get; set; }
    public Bil Bil { get; set; }
    public Parkeringsplads Parkeringsplads { get; set; }
    public required DateTime IndkoerselTid { get; set; }
    public DateTime? UdkoerselTid { get; set; }
}
