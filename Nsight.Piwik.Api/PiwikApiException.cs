using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsight.Piwik.Api
{
    public class PiwikApiException : Exception
    {
        public PiwikApiException(Exception inner) : this(string.Empty, inner) { }
        public PiwikApiException(string message, Exception inner) : base(message, inner) { }
    }
}
