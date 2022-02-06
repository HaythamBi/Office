using OfficeReservation.Application.Interfaces;
using OfficeReservation.Domain.Data.Entities;
using OfficeReservation.Domain.Data.Models;
using System;
using System.Collections.Generic;

namespace OfficeReservation.Application.BusinessLogic
{
    public class CalculateRevenueBehavior : ICalculateRevenueStrategy
    {
        public int Capacity { get; set; }
        public OutputRevenues CalculateRevenueBySpecificedMonth(List<IReservations> reservations, DateTime inputDate)
        {
            decimal revenues = 0;
            Capacity = 0;
            foreach (var item in reservations)
            {
                decimal monthlyPrice = item.MonthlyPrice == 0 ? 0 : ((decimal)item.MonthlyPrice / DaysInMonth(inputDate));
                revenues += GetDaysIncludedInTheRange(inputDate, item) * monthlyPrice;
            }
            return new OutputRevenues(revenues, Capacity);

        }

        private int GetDaysIncludedInTheRange(DateTime inputDate, IReservations dateRange)
        {
            var daysInMoth = DaysInMonth(inputDate);
            if ((inputDate.Month > dateRange.EndDate.Month) || (inputDate.Month < dateRange.StartDate.Month))
            {
                Capacity += dateRange.Capacity;
                return 0;
            }


            //  2022-3 => [10/02/2022 - X/04/2022] return full month 
            if ((inputDate.Month > dateRange.StartDate.Month) &&
                    (inputDate.Month < dateRange.EndDate.Month))
                return daysInMoth;



            //  2022-3 => [10/03/2022 - X/04/2022] return full or part of the month 
            if ((inputDate.Month == dateRange.StartDate.Month) &&
                    (inputDate.Month < dateRange.EndDate.Month))
                return (daysInMoth - dateRange.StartDate.Day) + 1;



            //  2022-3 => [10/03/2022 - 28/03/2022] return part of the month 
            if ((inputDate.Month == dateRange.StartDate.Month) &&
                    (inputDate.Month == dateRange.EndDate.Month))
                return (dateRange.EndDate.Day - dateRange.StartDate.Day) + 1;



            // 2022-3 => [10/2/2022 - 15/3/2022] return part of the month 
            if ((inputDate.Month > dateRange.StartDate.Month) &&
                    (inputDate.Month == dateRange.EndDate.Month))
                return dateRange.EndDate.Day;


            return 0;
        }

        private static int DaysInMonth(DateTime inputDate)
        {
            return DateTime.DaysInMonth(inputDate.Year, inputDate.Month);
        }
    }
}
