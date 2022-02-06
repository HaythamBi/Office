using OfficeReservation.Domain.Data.Models;
using System;

namespace OfficeReservation.Application.Interfaces
{
    public interface ICalculateRentForGevinMonth
    {
        int GetDaysIncludedInDateRangeByDate(DateTime inputDate, IReservations dateRange);
    }
}
