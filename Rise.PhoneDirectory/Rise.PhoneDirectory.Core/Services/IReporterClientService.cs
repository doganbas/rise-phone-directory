using RabbitMQ.Client;

namespace Rise.PhoneDirectory.Core.Services
{
    public interface IReporterClientService : IDisposable
    {
        public IModel Connect();
    }
}
