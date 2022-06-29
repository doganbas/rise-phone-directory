using Rise.PhoneDirectory.Store.Abstract;
using Rise.PhoneDirectory.Store.Enums;

namespace Rise.PhoneDirectory.Store.Models
{
    public class Report : IEntity
    {
        public int ReportId { get; set; }

        public DateTime RequestTime { get; set; }

        public ReportStatus ReportStatus { get; set; }
    }
}
