using System;

namespace OfficeReservation.Domain.Data.Models
{
    public interface IReservations
    {
        int Capacity { get; set; }
        int MonthlyPrice { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
