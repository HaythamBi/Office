using OfficeReservation.Domain.Models;

namespace OfficeReservation.Domain.Data.Entities
{
    public class OutputRevenues : IOutputRevenues
    {
        public decimal Revenues { get; set; }
        public int Capacity { get; set; }
        public OutputRevenues(decimal revenues, int capacity)
        {
            Revenues = revenues;
            Capacity = capacity;
        }

    }
}
