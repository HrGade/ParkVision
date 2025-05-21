using System.ComponentModel.DataAnnotations.Schema;

namespace ParkVision.Server.Model;

public class Parkering
{
    public int ParkeringID { get; set; }

    [ForeignKey("Bil")]
    public required string Nummerplade { get; set; }

    public required Bil Bil { get; set; }

    [ForeignKey("Parkeringsplads")]
    public required int ParkeringspladsID { get; set; }

    public Parkeringsplads Parkeringsplads { get; set; }
    public required DateTime IndkoerselTid { get; set; }
    public DateTime? UdkoerselTid { get; set; }
}
