using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nsight.Piwik.Api.Tests
{
    [TestClass]
    public class PiwikEventInfoTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PiwikEventInfo_Null_Throw1()
        {
            new PiwikEventInfo("url", null, "action");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PiwikEventInfo_Null_Throw2()
        {
            new PiwikEventInfo("url", "category", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PiwikEventInfo_Empty_Throw1()
        {
            new PiwikEventInfo("url", string.Empty, "action");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PiwikEventInfo_Empty_Throw2()
        {
            new PiwikEventInfo("url", "category", string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PiwikEventInfo_Spaces_Throw1()
        {
            new PiwikEventInfo("url", "  ", "action");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PiwikEventInfo_Spaces_Throw2()
        {
            new PiwikEventInfo("url", "category", "  ");
        }
    }
}
