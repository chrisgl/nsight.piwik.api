using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nsight.Piwik.Api.Tests
{
    [TestClass]
    public class PiwikEnvironmentInfoTests
    {
        [TestMethod]
        public void PiwikEnvironmentInfo_UserAgentString_Basic()
        {
            var info = new PiwikEnvironmentInfo();

            Assert.AreEqual("Mozilla/5.0 like Gecko", info.UserAgentString);
        }

        [TestMethod]
        public void PiwikEnvironmentInfo_UserAgentString_DeviceInfo()
        {
            var info = new PiwikEnvironmentInfo()
            {
                DeviceName = "device name",
                DeviceType = "device type",
                OperatingSystem = "operating system"
            };

            Assert.AreEqual("Mozilla/5.0 (operating system; device name; device type) like Gecko", info.UserAgentString);
        }
    }
}
