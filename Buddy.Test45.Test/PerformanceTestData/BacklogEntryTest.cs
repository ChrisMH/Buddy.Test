using Buddy.Test.PerformanceTestData;
using NUnit.Framework;

namespace Buddy.Test45.Test.TestData
{
    public class BacklogEntryTest
    {
        [Test]
        public void CanLoadPerformanceSnapshot()
        {
            var ps = BacklogEntry.Load();

            Assert.Greater(ps.Count, 0);
        }       
    }
}