using System;
using System.Runtime.CompilerServices;

namespace CallerInformation
{
    class Program
    {
        static void DoProcessing()
        {
            TraceMessage("Hello from DoProcessing.");
        }

        static void TraceMessage(string message,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            Console.WriteLine("message: " + message);
            Console.WriteLine("member name: " + memberName);
            Console.WriteLine("source file path: " + sourceFilePath);
            Console.WriteLine("source line number: " + sourceLineNumber);
            Console.WriteLine(new string('-',25));
        }

        static void Main()
        {
            //TraceMessage("Hello from Main!");
            DoProcessing();

            Console.ReadKey();
        }
    }
}
