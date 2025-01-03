using System.Collections.Generic;

namespace PythonTaskScheduler.Helpers
{
    public static class TimeMapping
    {
        public static readonly Dictionary<string, int> TimeToMinutes = new Dictionary<string, int>
        {
            { "1分鐘", 1 },
            { "3分鐘", 3 },
            { "5分鐘", 5 },
            { "1小時", 60 },
            { "2小時", 120 },
            { "6小時", 360 },
            { "12小時", 720 },
            { "1天", 1440 },
            { "1週", 10080 }
        };
    }
}