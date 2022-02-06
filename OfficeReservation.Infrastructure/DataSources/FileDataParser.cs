using OfficeReservation.Domain.Data;
using OfficeReservation.Domain.Exceptions;
using System;

namespace OfficeReservation.Infrastructure.DataSources
{
    class FileDataParser
    {
        public Reservations Parse(string[] dataPerOffice)
        {
            try
            {
                return new Reservations
                {
                    Capacity = Int16.Parse(dataPerOffice[0]),
                    MonthlyPrice = Int16.Parse(dataPerOffice[1]),
                    StartDate = DateTime.Parse(dataPerOffice[2] ?? DateTime.MinValue.ToString()),
                    EndDate = DateTime.Parse(String.IsNullOrEmpty(dataPerOffice[3]) == true ? DateTime.MaxValue.ToString() : dataPerOffice[3]),
                };
            }
            catch (Exception e)
            {
                throw new InvalidFileException("File Format is invalid");
            }

        }
    }
}
