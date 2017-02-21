using System.Collections.Generic;

namespace Buddy.Test.PerformanceTestData
{
    public static class Db
    {
        private static List<BacklogEntry> _backlog;
        public static List<BacklogEntry> Backlog => _backlog ?? (_backlog = BacklogEntry.Load());

        private static List<PerformanceEntry> _performance;
        public static List<PerformanceEntry> Performance => _performance ?? (_performance = PerformanceEntry.Load());
    }
}