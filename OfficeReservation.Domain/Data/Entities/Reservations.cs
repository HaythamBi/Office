using OfficeReservation.Domain.Data.Models;
using System;

namespace OfficeReservation.Domain.Data
{
    public class Reservations : IReservations
    {
        public int Capacity { get; set; }

        public int MonthlyPrice { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
