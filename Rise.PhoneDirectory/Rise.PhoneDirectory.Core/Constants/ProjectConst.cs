namespace Rise.PhoneDirectory.Core.Constants
{
    public static class ProjectConst
    {
        public static string ProjectName = "Phone Directory API";
        public static string SchemeName = "Directory";


        public static string PutIdError = "Güncellenecek veride bulunan Id alanı ile gönderilen Id eşleşmemektedir.";
        public static string DeleteLogMessage = "'{0}' tablosundan veri silindi.";

        public static string ExcelReportExchangeName = "excel-direct-exchange";
        public static string ExcelReportExchangeType = "direct";
        public static string ExcelReportRouting = "excel-route-file";
        public static string ExcelReportQueueName = "excel-queue-file";

        public static string ExcelReportServiceConnectionStart = "Kuyruklama sistemi ile başlantı kuruldu...";
        public static string ExcelReportServiceConnectionEnd = "Kuyruklama sistemi ile başlantı sonlandırıldı...";
        public static string ExcelReportServiceRequestNew = "Yeni bir rapor isteği oluşturuldu ve kuyruğa eklendi.";

        public static string ExcelReportServiceCrated = "{0} Id'li rapor talebi başarı ile oluşturuldu.";
        public static string ExcelReportServiceCreateError = "{0} Id'li rapor oluşturulurken bir hata oluştu.";
    }
}