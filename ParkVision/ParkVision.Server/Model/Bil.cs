[assembly: CLSCompliant(true)]

namespace ParkVision.Server.Model;

public class Bil
{
    public string Nummerplade { get; set; }
    public int BilTypeID { get; set; }
    public int ModelID { get; set; }
}