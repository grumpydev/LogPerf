namespace LogPerf
{
    using System.Diagnostics;

    public interface IBasicLogger
    {
        void LogEntry(string entry);
    }

    public class BasicLogger : IBasicLogger
    {
        public void LogEntry(string entry)
        {
            if (!Settings.LogEnabled)
            {
                return;
            }

            Trace.WriteLine(entry);
        }
    }
}