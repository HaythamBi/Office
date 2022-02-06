using OfficeReservation.Application.Interfaces;
using OfficeReservation.Domain.Data.Models;
using OfficeReservation.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace OfficeReservation.Infrastructure.DataSources
{
    public class DataFromLocalFile : IDataRepository
    {
        private IConfigurations _configurations;
        private FileDataParser _fileDataParser;

        public DataFromLocalFile(IConfigurations configurations)
        {
            _configurations = configurations;
            _fileDataParser = new FileDataParser();
        }
        public List<IReservations> GetReservationDataFromResource()
        {
            var reservations = new List<IReservations>();
            try
            {

                StringReader dataLine = new StringReader(_configurations.Data);
                var row = dataLine.ReadLine();
                while (row != null)
                {
                    row = dataLine.ReadLine();
                    if (row == null) break;
                    var line = row.Split(',');
                    if (Char.IsLetter(line[0][0])) continue;
                    reservations.Add(_fileDataParser.Parse(line));
                }
                return reservations;
            }
            catch (InvalidFileException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
