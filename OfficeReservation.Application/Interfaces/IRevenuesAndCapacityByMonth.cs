using OfficeReservation.Domain.Data.Entities;
using OfficeReservation.Domain.Data.Models;
using System;
using System.Collections.Generic;

namespace OfficeReservation.Application.Interfaces
{
    public interface IRevenuesAndCapacityByMonth
    {
        OutputRevenues Execute(DateTime dateTime);

        public List<IReservations> Cached { get; set; }

    }
}
