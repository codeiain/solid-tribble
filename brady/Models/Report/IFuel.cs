namespace brady.Models.Report
{
    public interface IFuel
    {
        string Name { get; set; }
        Generation Generation { get; set; }
        string EmissionsRating { get; set; }
    }
}