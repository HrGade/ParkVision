namespace ParkVision.Server.Model;

/// <summary>
/// DTO model som vi benyttes i tilfælde af, at vi gerne vil modtage 
/// en bestemt type information fra API-kalderen, dvs. vi specifierer 
/// JSON schema, når det kommer til POST requests fra API-kalderen.
/// 
/// Årsag: En API-kalder behøver ikke at sende information om en parkeringslads
/// for at lave POST, de behøver kun dens ID for at oprette en parkering til 
/// en bestemt parkeringsplads.
/// </summary>
public class ParkeringDTOPost
{
    public required Bil Bil { get; set; }
    public required int ParkeringspladsID { get; set; }
    public required DateTime IndkoerselTid { get; set; }
    public DateTime? UdkoerselTid { get; set; }
}
