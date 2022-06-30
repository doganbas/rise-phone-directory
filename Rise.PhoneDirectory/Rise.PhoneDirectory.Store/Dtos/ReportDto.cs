using Rise.PhoneDirectory.Store.Enums;

namespace Rise.PhoneDirectory.Store.Dtos
{
    public class ReportDto : BaseDto<int>
    {
        public DateTime RequestTime { get; set; }

        public DateTime? CreatedTime { get; set; }

        public ReportStatus ReportStatus { get; set; }

        public string FilePath { get; set; }
    }
}