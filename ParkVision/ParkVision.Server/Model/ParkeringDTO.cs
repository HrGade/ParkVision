namespace ParkVision.Server.Model;

public class ParkeringDTO
{
    public required int ParkeringID { get; set; }
    public required string Nummerplade { get; set; }
    public required int ParkeringspladsID { get; set; }
    public required DateTime IndkoerselTid { get; set; }
    public DateTime? UdkoerselTid { get; set; }
}
