using OfficeReservation.Domain.Data.Models;
using System.Collections.Generic;

namespace OfficeReservation.Application.Interfaces
{
    public interface IDataRepository
    {
        List<IReservations> GetReservationDataFromResource();
    }
}
