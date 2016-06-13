using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nsight.Piwik.Api
{
    /// <summary>
    /// Piwik API options.
    /// </summary>
    public sealed class PiwikApiOptions
    {
        /// <summary>
        /// Gets or sets site ID.
        /// </summary>
        public uint SiteId { get; set; }

        /// <summary>
        /// Gets or sets a URL for Piwik endpoint.
        /// </summary>
        public Uri Endpoint { get; set; }

        /// <summary>
        /// Gets or sets HTTP handler for test purposes.
        /// </summary>
        internal HttpMessageHandler TestOnlyHttpMessageHandler { get; set; }

        /// <summary>
        /// Validates options.
        /// </summary>
        internal void Validate()
        {
            if (SiteId < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(SiteId));
            }

            if (null == Endpoint)
            {
                throw new ArgumentNullException(nameof(Endpoint));
            }
        }
    }
}
