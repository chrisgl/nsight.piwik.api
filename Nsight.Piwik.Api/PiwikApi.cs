using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nsight.Piwik.Api
{
    /// <summary>
    /// Piwik API implementation.
    /// </summary>
    /// <remarks>
    /// http://developer.piwik.org/api-reference/tracking-api
    /// </remarks>
    public sealed class PiwikApi : IPiwikApi
    {
        /// <summary>
        /// API options.
        /// </summary>
        private readonly PiwikApiOptions options;

        /// <summary>
        /// Random number generator.
        /// </summary>
        private readonly Random rnd = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="PiwikApi"/> class.
        /// </summary>
        /// <param name="options">API options.</param>
        public PiwikApi(PiwikApiOptions options)
        {
            if (null == options)
            {
                throw new ArgumentNullException(nameof(options));
            }
            options.Validate();

            this.options = options;
        }

        #region IPiwikApi Members

        public Task ReportViewAsync(PiwikViewInfo viewInfo)
        {
            if (null == viewInfo)
            {
                throw new ArgumentNullException(nameof(viewInfo));
            }

            return SendRequestAsync(viewInfo, PiwikDataConverter.GetViewInfoArgs(viewInfo));
        }

        public Task ReportEventAsync(PiwikEventInfo eventInfo)
        {
            if (null == eventInfo)
            {
                throw new ArgumentNullException(nameof(eventInfo));
            }

            return SendRequestAsync(eventInfo, PiwikDataConverter.GetEventInfoArgs(eventInfo));
        }

        #endregion

        /// <summary>
        /// Sends request to an API endpoint.
        /// </summary>
        /// <param name="eventInfo">Base event information.</param>
        /// <param name="extraArgs">Additional arguments.</param>
        private async Task SendRequestAsync(PiwikEventInfoBase eventInfo, IEnumerable<string> extraArgs)
        {
            if (null == eventInfo)
            {
                throw new ArgumentNullException(nameof(eventInfo));
            }
            if (null == extraArgs)
            {
                throw new ArgumentNullException(nameof(extraArgs));
            }

            var args = new List<string>
            {
                "rec=1",
                "apiv=1",
                $"idsite={options.SiteId}",
                $"rand={rnd.Next()}"
            };

            args.AddRange(PiwikDataConverter.GetBaseEventInfoArgs(eventInfo));

            args.AddRange(PiwikDataConverter.GetSessionInfoArgs(eventInfo.Session));

            args.AddRange(PiwikDataConverter.GetEnvironmentInfoArgs(eventInfo.EnvironmentInfo));

            args.AddRange(extraArgs);

            var requestUri = new UriBuilder(options.Endpoint)
            {
                Query = string.Join("&", args)
            };

            try
            {
                using (var http = new HttpClient(new ApplyUserAgentHttpMessageHandler(options.TestOnlyHttpMessageHandler ?? new HttpClientHandler(), eventInfo.EnvironmentInfo)))
                {
                    (await http.GetAsync(requestUri.Uri).ConfigureAwait(false)).EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                if (!options.SwallowExceptions)
                {
                    throw new PiwikApiException(ex);
                }
            }
        }

        /// <summary>
        /// HTTP request handler that modifies User-Agent header.
        /// </summary>
        private sealed class ApplyUserAgentHttpMessageHandler : DelegatingHandler
        {
            /// <summary>
            /// Environment info to get user agent string from.
            /// </summary>
            private readonly PiwikEnvironmentInfo environmentInfo;

            /// <summary>
            /// Initializes a new instance of the <see cref="ApplyUserAgentHttpMessageHandler"/> class.
            /// </summary>
            /// <param name="innerHandler">Inner handler.</param>
            /// <param name="environmentInfo">Environment info to get user agent string from.</param>
            public ApplyUserAgentHttpMessageHandler(HttpMessageHandler innerHandler, PiwikEnvironmentInfo environmentInfo)
                : base(innerHandler)
            {
                this.environmentInfo = environmentInfo;
            }

            #region DelegatingHandler Members

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (null != environmentInfo)
                {
                    request.Headers.Add("User-Agent", environmentInfo.UserAgentString);
                }

                return base.SendAsync(request, cancellationToken);
            }

            #endregion
        }
    }
}
