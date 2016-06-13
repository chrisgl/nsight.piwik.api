using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsight.Piwik.Api
{
    /// <summary>
    /// Screen resolution information.
    /// </summary>
    public sealed class PiwikScreenResolution
    {
        /// <summary>
        /// Gets or sets screen width.
        /// </summary>
        public ushort Width { get; set; }

        /// <summary>
        /// Gets or sets screen height.
        /// </summary>
        public ushort Height { get; set; }

        /// <summary>
        /// Gets or sets screen DPI.
        /// </summary>
        public ushort Dpi { get; set; }
    }
}
