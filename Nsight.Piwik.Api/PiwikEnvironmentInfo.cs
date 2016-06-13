using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsight.Piwik.Api
{
    /// <summary>
    /// Environment information.
    /// </summary>
    public sealed class PiwikEnvironmentInfo
    {
        /// <summary>
        /// Computed user agent string.
        /// </summary>
        private readonly Lazy<string> userAgentString;

        /// <summary>
        /// Initializes a new instance of the <see cref="PiwikEnvironmentInfo"/> class.
        /// </summary>
        public PiwikEnvironmentInfo()
        {
            userAgentString = new Lazy<string>(ComputeUserAgentString);
        }

        /// <summary>
        /// Gets or sets operating system name and version.
        /// </summary>
        public string OperatingSystem { get; set; }

        /// <summary>
        /// Gets or sets device name.
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// Gets or sets device type.
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// Gets or sets device screen resolution.
        /// </summary>
        public PiwikScreenResolution DeviceScreen { get; set; }

        /// <summary>
        /// Gets user agent string.
        /// </summary>
        internal string UserAgentString => userAgentString.Value;

        /// <summary>
        /// Computes user agent string.
        /// </summary>
        private string ComputeUserAgentString()
        {
            var userAgentOptions = new List<string>();

            if (!string.IsNullOrWhiteSpace(OperatingSystem))
            {
                userAgentOptions.Add(OperatingSystem);
            }
            if (!string.IsNullOrWhiteSpace(DeviceName))
            {
                userAgentOptions.Add(DeviceName);
            }
            if (!string.IsNullOrWhiteSpace(DeviceType))
            {
                userAgentOptions.Add(DeviceType);
            }

            if (userAgentOptions.Any())
            {
                return $"Mozilla/5.0 ({string.Join("; ", userAgentOptions)}) like Gecko";
            }

            return "Mozilla/5.0 like Gecko";
        }
    }
}
