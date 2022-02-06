using OfficeReservation.Domain.Data.Entities;
using OfficeReservation.Domain.Data.Models;
using System;
using System.Collections.Generic;

namespace OfficeReservation.Application.Interfaces
{
    public interface ICalculateRevenueStrategy
    {
        public OutputRevenues CalculateRevenueBySpecificedMonth(List<IReservations> reservations, DateTime dateTime);
    }
}
