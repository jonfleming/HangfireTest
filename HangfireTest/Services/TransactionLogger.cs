using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TransactionLogging
{
    public interface ITransactionLogger
    {
        public Task LongRunningProcess(String data);
    }

    public class TransactionLogger : ITransactionLogger
    {

        public async Task LongRunningProcess(String data)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            Console.WriteLine($"Processed '{data}'");
        }
    }
}