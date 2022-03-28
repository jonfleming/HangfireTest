using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hangfire;
using HangfireTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TransactionLogging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HangfireTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleServiceController : ControllerBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public SampleServiceController(
            IServiceScopeFactory serviceScopeFactory, 
            IBackgroundJobClient backgroundJobClient)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _backgroundJobClient = backgroundJobClient;
        }

        // POST: api/SampleService
        [HttpPost]
        [Consumes("text/plain")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Trigger([FromBody] String data)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var transactionLogger = scope.ServiceProvider.GetRequiredService<ITransactionLogger>();
                _backgroundJobClient.Enqueue(() => transactionLogger.LongRunningProcess(data));
            }
            
            return Ok("In progress..");
        }

    }
}
