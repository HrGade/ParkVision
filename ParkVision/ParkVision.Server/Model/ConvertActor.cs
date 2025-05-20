namespace ParkVision.Server.Model;


public static class ConvertActor
{
    public static Parkering ParkeringDTO2Parkering(ParkeringDTO parkeringDTO)
    {
        return new Parkering
        {
            ParkeringID = parkeringDTO.ParkeringID,
            Nummerplade = parkeringDTO.Bil.Nummerplade,
            Bil = parkeringDTO.Bil,
            ParkeringspladsID = parkeringDTO.Parkeringsplads.ParkeringspladsID,
            Parkeringsplads = parkeringDTO.Parkeringsplads,
            IndkoerselTid = parkeringDTO.IndkoerselTid,
            UdkoerselTid = parkeringDTO.UdkoerselTid
        };
    }

    public static ParkeringDTO Parkering2ParkeringDTO(Parkering parkering)
    {
        return new ParkeringDTO
        {
            ParkeringID = parkering.ParkeringID,
            Bil = parkering.Bil,
            Parkeringsplads = parkering.Parkeringsplads,
            IndkoerselTid = parkering.IndkoerselTid,
            UdkoerselTid = parkering.UdkoerselTid
        };
    }

    public static Parkering ParkeringDTOPost2Parkering(ParkeringDTOPost parkeringDTOPost)
    {
        return new Parkering
        {
            Nummerplade = parkeringDTOPost.Bil.Nummerplade,
            Bil = parkeringDTOPost.Bil,
            ParkeringspladsID = parkeringDTOPost.ParkeringspladsID,
            IndkoerselTid = parkeringDTOPost.IndkoerselTid,
            UdkoerselTid = parkeringDTOPost.UdkoerselTid
        };
    }
}
