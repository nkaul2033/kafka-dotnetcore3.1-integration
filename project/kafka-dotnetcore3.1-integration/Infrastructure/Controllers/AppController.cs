using Confluent.Kafka;
using Infrastructure.Handlers;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Controllers
{
    [Route("api/[controller]")]
    public class AppController : ControllerBase
    {
        private readonly ProducerConfig config;
        public AppController(ProducerConfig config)
        {
            this.config = config;

        }

        [HttpPost]
        [Route("PostAsync")]
        public async Task<ActionResult> PostAsync([FromBody] Root value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Serialize 
            string serializedApps = JsonConvert.SerializeObject(value);

            Console.WriteLine("========");
            Console.WriteLine("Info: App Controller => Post => Recieved a new file to process apps:");
            Console.WriteLine(serializedApps);
            Console.WriteLine("=========");

            var producer = new ProducerHandler(config, "app-upload-request-main");
            await producer.writeMessage(serializedApps);

            return Created("TransactionId", Guid.NewGuid().ToString());
        }
    }
}
