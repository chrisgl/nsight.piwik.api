using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsight.Piwik.Api
{
    /// <summary>
    /// Provides converters of API data structures to Piwik query string parameters.
    /// </summary>
    internal static class PiwikDataConverter
    {
        /// <summary>
        /// Gets arguments that carry base event information.
        /// </summary>
        /// <param name="eventInfo">Base event information.</param>
        public static IEnumerable<string> GetBaseEventInfoArgs(PiwikEventInfoBase eventInfo)
        {
            if (null == eventInfo)
            {
                return Enumerable.Empty<string>();
            }

            var args = new List<string>()
            {
                $"url={Uri.EscapeDataString(eventInfo.Url)}"
            };

            args.SafeAddStringArgument("urlref", eventInfo.ReferrerUrl);

            args.AddRange(new[] { $"h={eventInfo.LocalTimestamp.Hour}", $"m={eventInfo.LocalTimestamp.Minute}", $"s={eventInfo.LocalTimestamp.Second}" });

            return args;
        }

        /// <summary>
        /// Gets arguments that carry session information.
        /// </summary>
        /// <param name="session">Session information.</param>
        public static IEnumerable<string> GetSessionInfoArgs(PiwikSessionInfo session)
        {
            if (null == session)
            {
                return Enumerable.Empty<string>();
            }

            var args = new List<string>();

            args.SafeAddStringArgument("_id", session.UniqueVisitorId);
            args.Add(string.Format(CultureInfo.InvariantCulture, "_idvc={0}", session.VisitsCount));
            args.SafeAddStringArgument("uid", session.UserId);

            if (session.FirstVisit.HasValue)
            {
                args.Add(string.Format(CultureInfo.InvariantCulture, "_idts={0}", DateTimeToUnixTime(session.FirstVisit.Value)));
            }
            if (session.LastVisit.HasValue)
            {
                args.Add(string.Format(CultureInfo.InvariantCulture, "_viewts={0}", DateTimeToUnixTime(session.LastVisit.Value)));
            }

            return args;
        }

        /// <summary>
        /// Gets arguments that carry environment information.
        /// </summary>
        /// <param name="environmentInfo">Environment information.</param>
        public static IEnumerable<string> GetEnvironmentInfoArgs(PiwikEnvironmentInfo environmentInfo)
        {
            if (null == environmentInfo)
            {
                return Enumerable.Empty<string>();
            }

            var args = new List<string>();

            if (null != environmentInfo.DeviceScreen)
            {
                args.Add(string.Format(CultureInfo.InvariantCulture, "res={0}x{1}", environmentInfo.DeviceScreen.Width, environmentInfo.DeviceScreen.Height));
            }

            return args;
        }

        /// <summary>
        /// Gets arguments that carry view information.
        /// </summary>
        /// <param name="viewInfo">View information.</param>
        public static IEnumerable<string> GetViewInfoArgs(PiwikViewInfo viewInfo)
        {
            if (null == viewInfo)
            {
                return Enumerable.Empty<string>();
            }

            var args = new List<string>();

            args.SafeAddStringArgument("action_name", viewInfo.ViewName);
            if (viewInfo.ViewTime.HasValue)
            {
                args.Add(string.Format(CultureInfo.InvariantCulture, "gt_ms={0:f0}", viewInfo.ViewTime.Value.TotalMilliseconds));
            }

            return args;
        }

        /// <summary>
        /// Gets arguments that carry event information.
        /// </summary>
        /// <param name="eventInfo">Event information.</param>
        public static IEnumerable<string> GetEventInfoArgs(PiwikEventInfo eventInfo)
        {
            if (null == eventInfo)
            {
                return Enumerable.Empty<string>();
            }

            var args = new List<string>()
            {
                $"e_c={Uri.EscapeDataString(eventInfo.Category)}",
                $"e_a={Uri.EscapeDataString(eventInfo.Action)}"
            };

            args.SafeAddStringArgument("e_n", eventInfo.Name);
            if (eventInfo.Value.HasValue)
            {
                args.Add(string.Format(CultureInfo.InvariantCulture, "e_v={0:.##}", eventInfo.Value.Value));
            }

            return args;
        }

        /// <summary>
        /// Converts <see cref="DateTimeOffset"/> to Unix time.
        /// </summary>
        /// <param name="time">Timestamp.</param>
        private static ulong DateTimeToUnixTime(DateTimeOffset time)
        {
            var epochTime = time.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToUInt32(epochTime.TotalSeconds);
        }
    }
}
