namespace Rise.PhoneDirectory.Core.Constants
{
    public static class ProjectConst
    {
        public const string ProjectName = "Phone Directory API";
        public const string SchemeName = "Directory";


        public const string PutIdError = "Güncellenecek veride bulunan Id alanı ile gönderilen Id eşleşmemektedir.";
        public const string DeleteNotFoundError = "Silinmek istenen obje veri tabanında bulunamadı.";
        public const string DeleteLogMessage = "'{0}' tablosundan veri silindi.";
        public const string GetReportExcelLogMessage = "Yeni bir rapor talebi oluşturularak kuyruğa eklendi.";
        public const string GetReportUploadLogMessage = "Rapor servisinden yeni bir rapor iletildi.";
        public const string GetReportDataLogMessage = "Rapor verilerine ulaşıldı.";

        public const string ExcelReportExchangeName = "excel-direct-exchange";
        public const string ExcelReportExchangeType = "direct";
        public const string ExcelReportRouting = "excel-route-file";
        public const string ExcelReportQueueName = "excel-queue-file";

        public const string ExcelReportServiceConnectionStart = "Kuyruklama sistemi ile başlantı kuruldu...";
        public const string ExcelReportServiceConnectionEnd = "Kuyruklama sistemi ile başlantı sonlandırıldı...";
        public const string ExcelReportServiceRequestNew = "Yeni bir rapor isteği oluşturuldu ve kuyruğa eklendi.";

        public const string ExcelReportServiceCrated = "{0} Id'li rapor talebi başarı ile oluşturuldu.";
        public const string ExcelReportServiceCreateError = "{0} Id'li rapor oluşturulurken bir hata oluştu.";

        public const string WrongValidationType = "Hatalı doğrulama sınıfı seçildi.";
    }
}