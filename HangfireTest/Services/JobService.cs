using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HangfireTest.Services
{
    public interface IJobService
    {
        void FireAndForgetJob();
    }

    public class JobService : IJobService
    {
        public void FireAndForgetJob()
        {
            this.CalledFromBackground();
        }

        public void CalledFromBackground()
        {
            int j = 5;
            for (int i = 0; i < j; i++)
            {
                Console.WriteLine($"{i}: Hello from a Fire and Forget job!");
                Thread.Sleep(1000);
            }

        }
    }
}
