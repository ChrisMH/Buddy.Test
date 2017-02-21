using Buddy.Test.PerformanceTestData;
using NUnit.Framework;

namespace Buddy.Test45.Test.TestData
{
    public class PerformanceEntryTest
    {
        [Test]
        public void CanLoadPerformanceSnapshot()
        {
            var ps = PerformanceEntry.Load();

            Assert.Greater(ps.Count, 0);
        }       
    }
}