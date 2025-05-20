namespace ParkVision.Server.Model;

public class ParkeringDTOPost
{
    public required Bil Bil { get; set; }
    public required int ParkeringspladsID { get; set; }
    public required DateTime IndkoerselTid { get; set; }
    public DateTime? UdkoerselTid { get; set; }
}
