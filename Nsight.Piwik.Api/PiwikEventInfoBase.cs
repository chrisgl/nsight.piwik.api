using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsight.Piwik.Api
{
    /// <summary>
    /// Base event information.
    /// </summary>
    public abstract class PiwikEventInfoBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PiwikEventInfoBase"/> class.
        /// </summary>
        /// <param name="url">URL of a view to be reported.</param>
        public PiwikEventInfoBase(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(nameof(url));
            }

            Url = url;
        }

        /// <summary>
        /// Gets URL of a view.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Gets or sets referrer URL.
        /// </summary>
        public string ReferrerUrl { get; set; }

        /// <summary>
        /// Gets or sets local timestamp of the event.
        /// </summary>
        /// <remarks>
        /// Defaults to current time.
        /// </remarks>
        public DateTime LocalTimestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets current session information.
        /// </summary>
        public PiwikSessionInfo Session { get; set; }

        /// <summary>
        /// Gets or sets current environment information.
        /// </summary>
        public PiwikEnvironmentInfo EnvironmentInfo { get; set; }
    }
}
