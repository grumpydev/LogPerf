namespace LogPerf
{
    using System;
    using System.Diagnostics;

    public class CallbackLogger
    {
        private readonly IBasicLogger logger = new RealLogger();

        private class RealLogger : IBasicLogger
        {
            public void LogEntry(string entry)
            {
                Trace.WriteLine(entry);
            }
        }

        public void WriteLog(Action<IBasicLogger> logCallback)
        {
            if (!Settings.LogEnabled)
            {
                return;
            }

            logCallback.Invoke(this.logger);
        }
    }
}