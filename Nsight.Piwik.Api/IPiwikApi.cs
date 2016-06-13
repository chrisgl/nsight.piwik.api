using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsight.Piwik.Api
{
    /// <summary>
    /// Piwik API contract.
    /// </summary>
    public interface IPiwikApi
    {
        /// <summary>
        /// Reports view.
        /// </summary>
        /// <param name="viewInfo">View information.</param>
        Task ReportViewAsync(PiwikViewInfo viewInfo);

        /// <summary>
        /// Reports event.
        /// </summary>
        /// <param name="eventInfo">Event information.</param>
        Task ReportEventAsync(PiwikEventInfo eventInfo);
    }
}
