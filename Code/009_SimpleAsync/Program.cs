using System;
using System.Threading;
using System.Threading.Tasks;

namespace _009_SimpleAsync
{
    public class AsyncClass
    {
        protected virtual async Task<string> GetDataAsync()
        {
            string s = "Hello";
            await Task.Delay(1000);
            
            return s+"1";
        }

        public async void PrintData()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            string result = await GetDataAsync();
            Console.WriteLine(result + "__" + Thread.CurrentThread.ManagedThreadId);
        }
    }

    static class Program
    {
        static void Main()
        {
            //var instance = new AsyncClass();

            //instance.PrintData();
            //for (int i = 0; i < 80; i++)
            //{
            //    Console.Write('.');
            //    Thread.Sleep(20);
            //}

int i,j;
j = 10;
i = j++ - j++;
rintf("%d %d", i,j);
}
        }
    }
}
