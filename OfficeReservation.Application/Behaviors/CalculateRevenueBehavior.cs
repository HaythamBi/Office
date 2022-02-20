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
                decimal monthlyPricePerDay = item.MonthlyPrice == 0 ? 0 : ((decimal)item.MonthlyPrice / DaysInMonth(inputDate));
                revenues += GetDaysIncludedInTheRange(inputDate, item) * monthlyPricePerDay;
            }
            return new OutputRevenues(revenues, Capacity);

        }

        private int GetDaysIncludedInTheRange(DateTime inputDate, IReservations dateRange)
        {
            int daysToCalculate = 0;
            var daysInMoth = DaysInMonth(inputDate);
            if ((inputDate > dateRange.EndDate) || (inputDate < dateRange.StartDate))
            {
                Capacity += dateRange.Capacity;
                return 0;
            }

            //  2022-3 => [10/02/2021 - 13/04/2022] Return full 

            daysToCalculate = IsEnclosingTimePeriod(inputDate, dateRange, daysInMoth);
            if (daysToCalculate > -1) return daysToCalculate;


            //  2022-3 => [10/03/2022 - 13/04/2022] Return full or part of the month 
            daysToCalculate = IsEnclosingStartTouchingPeriod(inputDate, dateRange, daysInMoth);
            if (daysToCalculate > -1) return daysToCalculate;


            //  2022-3 => [10/03/2022 - 28/03/2022] Return part of the month 
            daysToCalculate = IsExactMatchPeriod(inputDate, dateRange);
            if (daysToCalculate > -1) return daysToCalculate;


            // 2022-3 => [10/2/2022 - 15/3/2022] Return part of the month 
            daysToCalculate = IsEnclosingEndTouchingPeriod(inputDate, dateRange);
            if (daysToCalculate > -1) return daysToCalculate;

            return -1;
        }

        private int IsEnclosingEndTouchingPeriod(DateTime inputDate, IReservations dateRange)
        {
            if ((inputDate.Year > dateRange.StartDate.Year) &&
                   (inputDate.Year == dateRange.EndDate.Year) && (inputDate.Month == dateRange.EndDate.Month))
                return dateRange.EndDate.Day;

            if ((inputDate.Year == dateRange.StartDate.Year) && (inputDate.Month > dateRange.StartDate.Month) &&
                   (inputDate.Year == dateRange.StartDate.Year) && (inputDate.Month == dateRange.EndDate.Month))
                return dateRange.EndDate.Day;
            
            return -1;
        }

        private int IsExactMatchPeriod(DateTime inputDate, IReservations dateRange)
        {
            if ((inputDate.Year == dateRange.StartDate.Year) && (inputDate.Month == dateRange.StartDate.Month)
               && (inputDate.Year == dateRange.EndDate.Year) && (inputDate.Month == dateRange.EndDate.Month))
                return (dateRange.EndDate.Day - dateRange.StartDate.Day) + 1;

            return -1;
        }

        private int IsEnclosingStartTouchingPeriod(DateTime inputDate, IReservations dateRange, int daysInMoth)
        {
            if (inputDate.Year == dateRange.StartDate.Year && inputDate.Month == dateRange.StartDate.Month
              && inputDate.Year < dateRange.EndDate.Year)
                return (daysInMoth - dateRange.StartDate.Day) + 1;

            if (inputDate.Year == dateRange.StartDate.Year && inputDate.Month == dateRange.StartDate.Month
                && inputDate.Year == dateRange.EndDate.Year && inputDate.Month < dateRange.EndDate.Month)
                return (daysInMoth - dateRange.StartDate.Day) + 1;

            return -1;
        }

        private int IsEnclosingTimePeriod(DateTime inputDate, IReservations dateRange, int daysInMoth)
        {
            if ((inputDate.Year > dateRange.StartDate.Year) && (inputDate.Year < dateRange.EndDate.Year))
                return daysInMoth;

            if ((inputDate.Year > dateRange.StartDate.Year) && (inputDate.Year <= dateRange.EndDate.Year)
                && (inputDate.Month < dateRange.EndDate.Month))
                return daysInMoth;

            if ((inputDate.Year == dateRange.StartDate.Year) && (inputDate.Month > dateRange.StartDate.Month)
                    && (inputDate.Year < dateRange.EndDate.Year))
                return daysInMoth;

            if ((inputDate.Year > dateRange.StartDate.Year) &&
                    (inputDate.Year == dateRange.EndDate.Year) && (inputDate.Month < dateRange.EndDate.Month))
                return daysInMoth;

            if ((inputDate.Year == dateRange.StartDate.Year) && (inputDate.Month > dateRange.StartDate.Month)
                && (inputDate.Year == dateRange.EndDate.Year) && (inputDate.Month < dateRange.EndDate.Month))
                return daysInMoth;

            return -1;
        }

        private static int DaysInMonth(DateTime inputDate)
        {
            return DateTime.DaysInMonth(inputDate.Year, inputDate.Month);
        }
    }
}
