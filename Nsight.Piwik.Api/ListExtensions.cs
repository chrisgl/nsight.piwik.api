using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsight.Piwik.Api
{
    /// <summary>
    /// Internal list extensions.
    /// </summary>
    internal static class ListExtensions
    {
        /// <summary>
        /// Adds an argument to the list, if argument value is not null or whitespace.
        /// </summary>
        /// <param name="list">List to add argument to.</param>
        /// <param name="name">Name of the argument.</param>
        /// <param name="value">Argument value.</param>
        public static void SafeAddStringArgument(this List<string> list, string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (!string.IsNullOrWhiteSpace(value))
            {
                list.Add($"{Uri.EscapeDataString(name)}={Uri.EscapeDataString(value)}");
            }
        }
    }
}
