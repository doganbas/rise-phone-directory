using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Core.Services;
using Rise.PhoneDirectory.Store.Dtos;
using System.Text;
using System.Text.Json;

namespace Rise.PhoneDirectory.ReportWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IReporterClientService _reporterClientService;
        private IModel _channel;

        public Worker(ILogger<Worker> logger, IReporterClientService reporterClientService)
        {
            _logger = logger;
            _reporterClientService = reporterClientService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _reporterClientService.Connect();
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(ProjectConst.ExcelReportQueueName, false, consumer);
            consumer.Received += Consumer_Received; ;

            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var reportExcelMessageDto = JsonSerializer.Deserialize<ReportExcelMessageDto>(Encoding.UTF8.GetString(@event.Body.ToArray()));
                //TODO Read Report Data
                //TODO Create Excel Service

                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ProjectConst.ExcelReportServiceCreateError);
            }
        }
    }
}