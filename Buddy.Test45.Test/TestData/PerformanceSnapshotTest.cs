using Buddy.Test.TestData;
using NUnit.Framework;

namespace Buddy.Test45.Test.TestData
{
    public class PerformanceSnapshotTest
    {
        [Test]
        public void CanLoadPerformanceSnapshot()
        {
            var ps = PerformanceSnapshot.Load();

            Assert.Greater(ps.Count, 0);
        }       
    }
}