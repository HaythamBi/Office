namespace OfficeReservation.Domain.Models
{
    public interface IOutputRevenues
    {
        decimal Revenues { get; set; }
        int Capacity { get; set; }
    }
}
