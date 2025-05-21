namespace ParkVision.Server.Model;

/// <summary>
/// DTO model som vi benyttes i tilfælde af, at vi gerne vil sende 
/// data tilbage til API-kalderen, men vi vil ikke sende dem ALT informationen on en parkering, 
/// kun det som er relevant for dem.
/// </summary>
public class ParkeringDTO
{
    public required int ParkeringID { get; set; }
    public required Bil Bil { get; set; }
    public required Parkeringsplads Parkeringsplads { get; set; }
    public required DateTime IndkoerselTid { get; set; }
    public DateTime? UdkoerselTid { get; set; }
}
