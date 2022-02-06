using OfficeReservation.Application.BusinessLogic;
using OfficeReservation.Domain.Data;
using OfficeReservation.Domain.Data.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace OfficeReservation.UnitTest
{
    public class CalculateRevenueBehaviorShould
    {
        private CalculateRevenueBehavior CalculateRevenueBehavior = new CalculateRevenueBehavior();

        [Fact]
        public void CalculateFullMonthShould()
        {
            List<IReservations> reservations = new();
            reservations.Add(new Reservations
                {
                Capacity = 5,
                MonthlyPrice = 601,
                StartDate = DateTime.Parse("2014-02-15"),
                EndDate = DateTime.Parse("2014-04-1")
                });

            reservations.Add(new Reservations
            {
                Capacity = 3,
                MonthlyPrice = 300,
                StartDate = DateTime.Parse("2014-09-15"),
                EndDate = DateTime.Parse("2014-11-1")
            });

            var inputDate = DateTime.Parse("2014-3");
          var output = CalculateRevenueBehavior.CalculateRevenueBySpecificedMonth(reservations, inputDate);
            Assert.NotNull(output);
            Assert.Equal(3, output.Capacity);
            Assert.Equal(601, (int)output.Revenues);
        }


        [Fact]
        public void CalculateHalfOfMonthFromRightPartOfDateRangeShould()
        {
            List<IReservations> reservations = new();
            reservations.Add(new Reservations
            {
                Capacity = 5,
                MonthlyPrice = 601,
                StartDate = DateTime.Parse("2014-02-15"),
                EndDate = DateTime.Parse("2014-04-1")
            });

            reservations.Add(new Reservations
            {
                Capacity = 3,
                MonthlyPrice = 300,
                StartDate = DateTime.Parse("2014-09-15"),
                EndDate = DateTime.Parse("2014-11-1")
            });

            var inputDate = DateTime.Parse("2014-2");
            var output = CalculateRevenueBehavior.CalculateRevenueBySpecificedMonth(reservations, inputDate);
            Assert.NotNull(output);
            Assert.Equal(3, output.Capacity);
            Assert.Equal(300, (int)output.Revenues);
        }


        [Fact]
        public void CalculateHalfOfMonthFromLeftPartOfDateRangeShould()
        {
            List<IReservations> reservations = new();
            reservations.Add(new Reservations
            {
                Capacity = 5,
                MonthlyPrice = 601,
                StartDate = DateTime.Parse("2014-02-15"),
                EndDate = DateTime.Parse("2014-04-20")
            });

            reservations.Add(new Reservations
            {
                Capacity = 3,
                MonthlyPrice = 300,
                StartDate = DateTime.Parse("2014-09-15"),
                EndDate = DateTime.Parse("2014-11-1")
            });

            var inputDate = DateTime.Parse("2014-4");
            var output = CalculateRevenueBehavior.CalculateRevenueBySpecificedMonth(reservations, inputDate);
            Assert.NotNull(output);
            Assert.Equal(3, output.Capacity);
            Assert.Equal(400, (int)output.Revenues);
        }


        [Fact]
        public void CalculateHalfOfMonthFromBothSidesOfDateRangeShould()
        {
            List<IReservations> reservations = new();
            reservations.Add(new Reservations
            {
                Capacity = 5,
                MonthlyPrice = 601,
                StartDate = DateTime.Parse("2014-04-3"),
                EndDate = DateTime.Parse("2014-04-30")
            });

            reservations.Add(new Reservations
            {
                Capacity = 3,
                MonthlyPrice = 300,
                StartDate = DateTime.Parse("2014-09-15"),
                EndDate = DateTime.Parse("2014-11-1")
            });

            var inputDate = DateTime.Parse("2014-4");
            var output = CalculateRevenueBehavior.CalculateRevenueBySpecificedMonth(reservations, inputDate);
            Assert.NotNull(output);
            Assert.Equal(3, output.Capacity);
            Assert.Equal(560, (int)output.Revenues);
        }


        [Fact]
        public void CalculateInputDateOutsideOfDateRangeShould()
        {
            List<IReservations> reservations = new();
            reservations.Add(new Reservations
            {
                Capacity = 5,
                MonthlyPrice = 601,
                StartDate = DateTime.Parse("2014-04-3"),
                EndDate = DateTime.Parse("2014-04-30")
            });

            reservations.Add(new Reservations
            {
                Capacity = 3,
                MonthlyPrice = 300,
                StartDate = DateTime.Parse("2014-09-15"),
                EndDate = DateTime.Parse("2014-11-1")
            });

            var inputDate = DateTime.Parse("2014-1");
            var output = CalculateRevenueBehavior.CalculateRevenueBySpecificedMonth(reservations, inputDate);
            Assert.NotNull(output);
            Assert.Equal(8, output.Capacity);
            Assert.Equal(0, (int)output.Revenues);
        }
    }
}
