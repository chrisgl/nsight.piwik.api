using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nsight.Piwik.Api.Tests
{
    [TestClass]
    public class PiwikDataConverterTests
    {
        private const string TestUrl = "https://www.test.com/qwe?asd=zxc";

        private readonly DateTimeOffset testTimestamp = new DateTimeOffset(2016, 6, 5, 10, 20, 30, TimeSpan.Zero);

        [TestMethod]
        public void GetBaseEventInfoArgs_NullArgs()
        {
            var result = PiwikDataConverter.GetBaseEventInfoArgs(null);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetBaseEventInfoArgs_MinimalArgs()
        {
            var info = new PiwikViewInfo(TestUrl);

            var result = PiwikDataConverter.GetBaseEventInfoArgs(WithTestTimestamp(info));

            Assert.IsNotNull(result);
            AssertEqual(result, new[] { "url=https%3A%2F%2Fwww.test.com%2Fqwe%3Fasd%3Dzxc", "h=10", "m=20", "s=30" });
        }

        [TestMethod]
        public void GetBaseEventInfoArgs_AllArgs()
        {
            var info = new PiwikViewInfo(TestUrl)
            {
                ReferrerUrl = "https://www.example.com"
            };

            var result = PiwikDataConverter.GetBaseEventInfoArgs(WithTestTimestamp(info));

            Assert.IsNotNull(result);
            AssertEqual(result, new[] { "url=https%3A%2F%2Fwww.test.com%2Fqwe%3Fasd%3Dzxc", "urlref=https%3A%2F%2Fwww.example.com", "h=10", "m=20", "s=30" });
        }

        [TestMethod]
        public void GetSessionInfoArgs_NullArgs()
        {
            var result = PiwikDataConverter.GetSessionInfoArgs(null);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetSessionInfoArgs_MinimalArgs()
        {
            var info = new PiwikSessionInfo();

            var result = PiwikDataConverter.GetSessionInfoArgs(info);

            Assert.IsNotNull(result);
            AssertEqual(result, new[] { "_idvc=0" });
        }

        [TestMethod]
        public void GetSessionInfoArgs_AllArgs()
        {
            var info = new PiwikSessionInfo()
            {
                VisitsCount = 123,
                UniqueVisitorId = "unique id",
                UserId = "user id",
                FirstVisit = new DateTimeOffset(1970, 1, 1, 0, 0, 5, TimeSpan.Zero),
                LastVisit = new DateTimeOffset(1970, 1, 1, 0, 0, 40, TimeSpan.Zero)
            };

            var result = PiwikDataConverter.GetSessionInfoArgs(info);

            Assert.IsNotNull(result);
            AssertEqual(result, new[] { "_id=unique%20id", "_idvc=123", "uid=user%20id", "_idts=5", "_viewts=40" });
        }

        [TestMethod]
        public void GetEnvironmentInfoArgs_NullArgs()
        {
            var result = PiwikDataConverter.GetEnvironmentInfoArgs(null);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetEnvironmentInfoArgs_MinimalArgs()
        {
            var info = new PiwikEnvironmentInfo();

            var result = PiwikDataConverter.GetEnvironmentInfoArgs(info);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetEnvironmentInfoArgs_AllArgs()
        {
            var info = new PiwikEnvironmentInfo()
            {
                DeviceName = "device name",
                DeviceType = "device type",
                OperatingSystem = "operating system",
                DeviceScreen = new PiwikScreenResolution()
                {
                    Height = 1230,
                    Width = 4560,
                    Dpi = 789
                }
            };

            var result = PiwikDataConverter.GetEnvironmentInfoArgs(info);

            Assert.IsNotNull(result);
            AssertEqual(result, new[] { "res=4560x1230" });
        }

        [TestMethod]
        public void GetViewInfoArgs_NullArgs()
        {
            var result = PiwikDataConverter.GetViewInfoArgs(null);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetViewInfoArgs_MinimalArgs()
        {
            var info = new PiwikViewInfo(TestUrl);

            var result = PiwikDataConverter.GetViewInfoArgs(info);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetViewInfoArgs_AllArgs()
        {
            var info = new PiwikViewInfo(TestUrl)
            {
                ViewName = "view name",
                ViewTime = TimeSpan.FromMilliseconds(1230.4)
            };

            var result = PiwikDataConverter.GetViewInfoArgs(info);

            Assert.IsNotNull(result);
            AssertEqual(result, new[] { "action_name=view%20name", "gt_ms=1230" });
        }

        [TestMethod]
        public void GetEventInfoArgs_NullArgs()
        {
            var result = PiwikDataConverter.GetEventInfoArgs(null);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetEventInfoArgs_MinimalArgs()
        {
            var info = new PiwikEventInfo(TestUrl, "test category", "test action");

            var result = PiwikDataConverter.GetEventInfoArgs(info);

            Assert.IsNotNull(result);
            AssertEqual(result, new[] { "e_c=test%20category", "e_a=test%20action" });
        }

        [TestMethod]
        public void GetEventInfoArgs_AllArgs_ValueInt()
        {
            var info = new PiwikEventInfo(TestUrl, "test category", "test action")
            {
                Name = "event name",
                Value = 1230
            };

            var result = PiwikDataConverter.GetEventInfoArgs(info);

            Assert.IsNotNull(result);
            AssertEqual(result, new[] { "e_c=test%20category", "e_a=test%20action", "e_n=event%20name", "e_v=1230" });
        }

        [TestMethod]
        public void GetEventInfoArgs_AllArgs_ValueFloat()
        {
            var info = new PiwikEventInfo(TestUrl, "test category", "test action")
            {
                Name = "event name",
                Value = 1230.456f
            };

            var result = PiwikDataConverter.GetEventInfoArgs(info);

            Assert.IsNotNull(result);
            AssertEqual(result, new[] { "e_c=test%20category", "e_a=test%20action", "e_n=event%20name", "e_v=1230.46" });
        }

        private void AssertEqual(IEnumerable<string> seq1, IEnumerable<string> seq2)
        {
            var diff = Enumerable.Except(seq1, seq2);
            Assert.IsFalse(diff.Any());
        }

        private PiwikEventInfoBase WithTestTimestamp(PiwikEventInfoBase info)
        {
            info.LocalTimestamp = testTimestamp.UtcDateTime;
            return info;
        }
    }
}
