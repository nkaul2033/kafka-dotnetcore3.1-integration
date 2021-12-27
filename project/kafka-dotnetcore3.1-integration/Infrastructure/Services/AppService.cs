using Infrastructure;

namespace Infrastructure.Services
{
    using Microsoft.Extensions.Hosting;
    using System.Threading;
    using System.Threading.Tasks;
    using System;
    using Newtonsoft.Json;
    using Confluent.Kafka;
    using Infrastructure.Models;
    using Infrastructure.Handlers;

    public class AppService: IAppService 
    {
        private readonly ConsumerConfig consumerConfig;
        private readonly ProducerConfig producerConfig;
        public AppService(ConsumerConfig consumerConfig, ProducerConfig producerConfig)
        {
            this.producerConfig = producerConfig;
            this.consumerConfig = consumerConfig;
        }
        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("App Processing Service Started");

            
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumerHelper = new ConsumerHandler(consumerConfig, "app-upload-request-main");
                string orderRequest = consumerHelper.readMessage();

                //Deserilaize 
                Root apps = JsonConvert.DeserializeObject<Root>(orderRequest);

                var producerWrapper = new ProducerHandler(producerConfig, "app-upload-request-secondary");
                foreach (RootApp app in apps.rootApps) {
                    Console.WriteLine($"Info: OrderHandler => Processing the app upload for {app.first_name}");
                    await producerWrapper.writeMessage(JsonConvert.SerializeObject(app));
                }
            }
        }
    }

    public interface IAppService
    {
        public Task ExecuteAsync(CancellationToken stoppingToken);
    }
}