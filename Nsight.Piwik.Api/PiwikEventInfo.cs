using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsight.Piwik.Api
{
    /// <summary>
    /// Represents event information.
    /// </summary>
    public sealed class PiwikEventInfo : PiwikEventInfoBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PiwikEventInfo"/> class.
        /// </summary>
        /// <param name="url">URL of a view where action had occurred.</param>
        /// <param name="category">Event category.</param>
        /// <param name="action">Event action.</param>
        public PiwikEventInfo(string url, string category, string action)
            : base(url)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException(nameof(category));
            }
            if (string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentException(nameof(action));
            }

            Category = category;
            Action = action;
        }

        /// <summary>
        /// Gets event category (e.g. Videos, Music, Games...).
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// Gets event action (e.g. Play, Pause...).
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// Gets or sets event name (e.g. file name, song title...).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a float value associated with an event.
        /// </summary>
        public float? Value { get; set; } = null;
    }
}
