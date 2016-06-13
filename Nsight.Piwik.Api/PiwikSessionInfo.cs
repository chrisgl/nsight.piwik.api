using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsight.Piwik.Api
{
    /// <summary>
    /// Session information.
    /// </summary>
    public sealed class PiwikSessionInfo
    {
        /// <summary>
        /// Gets or sets unique visitor ID (16 character hexadecimal string).
        /// </summary>
        public string UniqueVisitorId { get; set; }

        /// <summary>
        /// Gets or sets user ID associated with unique visitor ID, if available (e.g. email, user name...).
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets timestamp of the first visit.
        /// </summary>
        public DateTimeOffset? FirstVisit { get; set; }

        /// <summary>
        /// Gets or sets timestamp of the last visit.
        /// </summary>
        public DateTimeOffset? LastVisit { get; set; }

        /// <summary>
        /// Gets or sets number of visits user had made so far.
        /// </summary>
        public uint VisitsCount { get; set; }
    }
}
