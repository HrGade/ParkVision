namespace ParkVision.Server.Model;

/// <summary>
/// 
/// </summary>
public class ParkeringDTO
{
    public required int ParkeringID { get; set; }
    public required Bil Bil { get; set; }
    public required Parkeringsplads Parkeringsplads { get; set; }
    public required DateTime IndkoerselTid { get; set; }
    public DateTime? UdkoerselTid { get; set; }
}
