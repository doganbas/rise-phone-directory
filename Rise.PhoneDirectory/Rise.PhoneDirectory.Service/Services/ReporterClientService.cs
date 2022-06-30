using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Rise.PhoneDirectory.Core.Aspects;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Core.Services;

namespace Rise.PhoneDirectory.Service.Services
{
    [ExceptionLogAspect]
    public class ReporterClientService : IReporterClientService
    {
        private readonly ILogger<ReporterClientService> _logger;
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public ReporterClientService(ILogger<ReporterClientService> logger, ConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
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