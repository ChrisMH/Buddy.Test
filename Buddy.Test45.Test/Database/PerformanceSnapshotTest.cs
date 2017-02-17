using Buddy.Test.Database;
using NUnit.Framework;

namespace Buddy.Test.Test.Database
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