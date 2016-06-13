using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Nsight.Piwik.Api.Tests
{
    [TestClass]
    public class PiwikApiTests
    {
        private TestHttpMessageHandler httpFailureHandler;

        [TestInitialize]
        public void Initialize()
        {
            httpFailureHandler = new TestHttpMessageHandler()
            {
                Send = r => new HttpResponseMessage(HttpStatusCode.BadRequest)
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorThrows_NoOptions()
        {
            new Api.PiwikApi(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void CtorThrows_InvalidOptions()
        {
            new Api.PiwikApi(new PiwikApiOptions());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReportViewAsyncThrows_NoEventArgs()
        {
            CreateNewApiInstance().ReportViewAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReportEventAsyncThrows_NoEventArgs()
        {
            CreateNewApiInstance().ReportEventAsync(null);
        }

        [TestMethod]
        public async Task ReportViewAsyncThrows_HttpFailure()
        {
            var correctExceptionThrown = false;

            try
            {
                await CreateNewApiInstance(httpFailureHandler)
                    .ReportViewAsync(new PiwikViewInfo("https://www.test.com"));
            }
            catch (PiwikApiException ex) when (ex.InnerException is HttpRequestException)
            {
                correctExceptionThrown = true;
            }

            Assert.IsTrue(correctExceptionThrown);
        }

        [TestMethod]
        public async Task ReportEventAsyncThrows_HttpFailure()
        {
            var correctExceptionThrown = false;

            try
            {
                await CreateNewApiInstance(httpFailureHandler)
                    .ReportEventAsync(new PiwikEventInfo("https://www.test.com", "category", "action"));
            }
            catch (PiwikApiException ex) when (ex.InnerException is HttpRequestException)
            {
                correctExceptionThrown = true;
            }

            Assert.IsTrue(correctExceptionThrown);
        }

        [TestMethod]
        public async Task ExceptionsCanBeSwallowed()
        {
            var exceptionOccurred = false;

            try
            {
                await CreateNewApiInstance(httpFailureHandler, swallowExceptions: true)
                    .ReportViewAsync(new PiwikViewInfo("https://www.test.com"));
            }
            catch (Exception)
            {
                exceptionOccurred = true;
            }

            Assert.IsFalse(exceptionOccurred);
        }

        [TestMethod]
        public async Task SendRequestAsync_AddsUserAgentHeader()
        {
            var containsUserAgentHeader = false;

            var environmentInfo = new PiwikEnvironmentInfo()
            {
                DeviceName = "device name",
                DeviceType = "device type",
                OperatingSystem = "operating system"
            };

            var testHandler = new TestHttpMessageHandler()
            {
                Send = r =>
                {
                    containsUserAgentHeader = r.Headers.UserAgent.ToString() == environmentInfo.UserAgentString;
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            };

            await CreateNewApiInstance(testHandler)
                .ReportViewAsync(new PiwikViewInfo("https://www.test.com") { EnvironmentInfo = environmentInfo });

            Assert.IsTrue(containsUserAgentHeader);
        }

        private IPiwikApi CreateNewApiInstance(TestHttpMessageHandler handler = null, bool swallowExceptions = false)
        {
            return new Api.PiwikApi(new PiwikApiOptions()
            {
                SiteId = 123,
                Endpoint = new Uri("https://www.example.com/piwik"),
                TestOnlyHttpMessageHandler = handler,
                SwallowExceptions = swallowExceptions
            });
        }
    }
}
