using Rise.PhoneDirectory.Store.Enums;

namespace Rise.PhoneDirectory.Store.Dtos
{
    public class ReportDto : BaseDto<int>
    {
        public DateTime RequestTime { get; set; }

        public ReportStatus ReportStatus { get; set; }
    }
}