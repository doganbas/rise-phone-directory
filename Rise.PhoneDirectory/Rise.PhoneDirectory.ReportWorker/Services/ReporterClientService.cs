using RabbitMQ.Client;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Core.Services;

namespace Rise.PhoneDirectory.ReportWorker.Services
{
    public class ReporterClientService : IReporterClientService
    {
        private readonly ILogger<ReporterClientService> _logger;
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public ReporterClientService(ConnectionFactory connectionFactory, ILogger<ReporterClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();
            if (_channel is { IsOpen: true })
                return _channel;

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ProjectConst.ExcelReportExchangeName, type: ProjectConst.ExcelReportExchangeType, true, false);
            _channel.QueueDeclare(ProjectConst.ExcelReportQueueName, true, false, false, null);
            _channel.QueueBind(exchange: ProjectConst.ExcelReportExchangeName, queue: ProjectConst.ExcelReportQueueName, routingKey: ProjectConst.ExcelReportRouting);
            _logger.LogInformation(ProjectConst.ExcelReportServiceConnectionStart);

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation(ProjectConst.ExcelReportServiceConnectionEnd);
        }
    }
}