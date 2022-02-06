using OfficeReservation.Application.Interfaces;

namespace OfficeReservation.Infrastructure.Configurations
{
    public class Configurations : IConfigurations
    {
        public string PathOfLocalFile { get; set; }
        public string Data { get; set; }
    }
}
