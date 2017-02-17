using System.Collections.Generic;

namespace Buddy.Test.TestData
{
    public static class Data
    {
        private static List<PerformanceSnapshot> _performanceSnapshots;
        public static List<PerformanceSnapshot> PerformanceSnapshots
        {
            get { return _performanceSnapshots ?? (_performanceSnapshots = PerformanceSnapshot.Load()); }
        } 
    }
}