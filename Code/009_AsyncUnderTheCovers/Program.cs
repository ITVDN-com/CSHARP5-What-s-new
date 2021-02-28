using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace _009_AsyncUnderTheCovers
{

    public class AsyncClass
    {
        public Task<string> Method()
        {
            var stateMachine = new MethodStateMachine
                 {
                     // Represents a builder for asynchronous methods that return a task 
                     // and provides a parameter for the result.
                     builder = AsyncTaskMethodBuilder<string>.Create(),

                     state = -1
                 };
            stateMachine.builder.Start(ref stateMachine);
            return stateMachine.builder.Task;
        }

        private struct MethodStateMachine : IAsyncStateMachine
        {
            public int state;
            public AsyncTaskMethodBuilder<string> builder;
            private TaskAwaiter awaiter;

            void IAsyncStateMachine.MoveNext()
            {
                string result = null;

                try
                {
                    TaskAwaiter awaiter;
                    switch (state)
                    {
                        case -3:
                            goto label_6;

                        case 0:
                            awaiter = this.awaiter;
                            this.awaiter = new TaskAwaiter();
                            this.state = -1;
                            break;
                        default:
                            //Getting the Awaiter for the awaitable operation!

                            awaiter = Task.Delay(1000).GetAwaiter();

                            if (!awaiter.IsCompleted)
                            {
                                this.state = 0;
                                this.awaiter = awaiter;

                                // Schedules the state machine to proceed to the next action when 
                                // the specified awaiter completes.
                                this.builder.AwaitOnCompleted<TaskAwaiter, MethodStateMachine>(ref awaiter, ref this);
                                return;
                            }
                            else
                                break;
                    }
                    awaiter.GetResult();
                    TaskAwaiter taskAwaiter = new TaskAwaiter();
                    result = "Hello World";
                }
                catch (Exception ex)
                {
                    this.state = -2;
                    this.builder.SetException(ex);
                    return;
                }
            label_6:
                this.state = -2;
                this.builder.SetResult(result);
            }

            [DebuggerHidden]
            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
            {
                this.builder.SetStateMachine(param0);
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            var instance = new AsyncClass();

            Task<String> task = instance.Method();

            for (int i = 0; i < 80; i++)
            {
                Console.Write(".");
                Thread.Sleep(10);
            }
            Console.WriteLine(task.Result);
        }
    }
}
