using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsight.Piwik.Api
{
    /// <summary>
    /// Represents view information.
    /// </summary>
    public class PiwikViewInfo : PiwikEventInfoBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PiwikViewInfo"/> class.
        /// </summary>
        /// <param name="url">URL of a view.</param>
        public PiwikViewInfo(string url)
            : base(url)
        {
        }

        /// <summary>
        /// Gets or sets view name. Use "/" characters to indicate hierarchy.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets amount of time spent to generate the view.
        /// </summary>
        public TimeSpan? ViewTime { get; set; }
    }
}
