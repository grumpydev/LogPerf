namespace LogPerf
{
    using System;

    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    class Program
    {
        private const int Iterations = 1000000;

        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to start.");
            Console.ReadLine();
            Thread.Sleep(500);

            Console.WriteLine("Baseline - log log calls");
            RunNoLogger();
            GC.Collect();
            GC.WaitForFullGCComplete();
            Thread.Sleep(1000);

            Settings.LogEnabled = false;

            Console.WriteLine("Log Off / Basic");
            RunBasicLogger();
            GC.Collect();
            GC.WaitForFullGCComplete();
            Thread.Sleep(1000);

            Console.WriteLine("Log Off / Callback");
            RunCallbackLogger();
            GC.Collect();
            GC.WaitForFullGCComplete();
            Thread.Sleep(1000);

            Settings.LogEnabled = true;

            Console.WriteLine("Log On / Basic");
            RunBasicLogger();
            GC.Collect();
            GC.WaitForFullGCComplete();
            Thread.Sleep(1000);

            Console.WriteLine("Log On / Callback");
            RunCallbackLogger();
            GC.Collect();
            GC.WaitForFullGCComplete();
            Thread.Sleep(1000);

            Console.WriteLine("Press ENTER to quit");
            Console.ReadLine();
        }

        [MethodImplAttribute(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static void RunNoLogger()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < Iterations; i++)
            {
                var j = i;
            }

            stopwatch.Stop();
            Console.WriteLine("Ellapsed (ms): {0}", stopwatch.ElapsedMilliseconds);
            Console.WriteLine();
        }

        [MethodImplAttribute(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static void RunCallbackLogger()
        {
            var stopwatch = new Stopwatch();
            var logger = new CallbackLogger();
            stopwatch.Start();

            for (int i = 0; i < Iterations; i++)
            {
                var j = i;

                logger.WriteLog(l => l.LogEntry(String.Format("Callback Iteration: {0}", j)));
            }

            stopwatch.Stop();
            Console.WriteLine("Ellapsed (ms): {0}", stopwatch.ElapsedMilliseconds);
            Console.WriteLine();
        }

        [MethodImplAttribute(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static void RunBasicLogger()
        {
            var stopwatch = new Stopwatch();
            var logger = new BasicLogger();
            stopwatch.Start();

            for (int i = 0; i < Iterations; i++)
            {
                var j = i;

                logger.LogEntry(String.Format("Basic Iteration: {0}", j));
            }

            stopwatch.Stop();
            Console.WriteLine("Ellapsed (ms): {0}", stopwatch.ElapsedMilliseconds);
            Console.WriteLine();
        }
    }
}
