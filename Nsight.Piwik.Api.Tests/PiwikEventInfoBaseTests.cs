using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nsight.Piwik.Api.Tests
{
    [TestClass]
    public class PiwikEventInfoBaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PiwikEventInfoBase_Null_Throws()
        {
            new PiwikViewInfo(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PiwikEventInfoBase_Empty_Throws()
        {
            new PiwikViewInfo(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PiwikEventInfoBase_Spaces_Throws()
        {
            new PiwikViewInfo("  ");
        }
    }
}
