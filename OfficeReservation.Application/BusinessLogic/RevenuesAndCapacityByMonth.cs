using OfficeReservation.Application.Interfaces;
using OfficeReservation.Domain.Data.Entities;
using OfficeReservation.Domain.Data.Models;
using System;
using System.Collections.Generic;

namespace OfficeReservation.Application.BusinessLogic
{
    public class RevenuesAndCapacityByMonth : IRevenuesAndCapacityByMonth
    {
        private IDataRepository _dataRepository;
        private ICalculateRevenueStrategy _calculateRevenueStrategy;
        public List<IReservations> Cached { get; set; }
        public RevenuesAndCapacityByMonth(IDataRepository dataRepository)
        { 
            _dataRepository = dataRepository;
        }


        public OutputRevenues Execute(DateTime dateTime)
        {
            _calculateRevenueStrategy = new CalculateRevenueBehavior();
            var reservations = Cached = _dataRepository.GetReservationDataFromResource();
            var results = _calculateRevenueStrategy.CalculateRevenueBySpecificedMonth(reservations, dateTime);
            return results;
        }

    }
}
