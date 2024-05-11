using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.ExtensionMethods
{
    /// <summary>
    /// Contains Extension Methods for the <see cref="TimeSpan"/> type
    /// </summary>
    public static class TimeSpanExtension
    {
        /// <summary>
        /// Format a <see cref="TimeSpan"/> to a string<br/>
        /// In the format string, the following are replaced:<br/><br/>
        /// DD -> <see cref="TimeSpan.Days"/><br/>
        /// HH -> <see cref="TimeSpan.Hours"/><br/>
        /// MM -> <see cref="TimeSpan.Minutes"/><br/>
        /// SS -> <see cref="TimeSpan.Seconds"/><br/>
        /// ms -> <see cref="TimeSpan.Milliseconds"/><br/>
        /// mcs -> <see cref="TimeSpan.Microseconds"/>
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <param name="formatString"></param>
        /// <returns></returns>
        public static string ToString(this TimeSpan? timeSpan, string formatString)
        {
            if(timeSpan == null)
                return string.Empty;

            formatString = formatString.Replace("DD", timeSpan.Value.Days.ToString());
            formatString = formatString.Replace("HH", timeSpan.Value.Hours.ToString());

            string minutes = timeSpan.Value.Minutes.ToString();
            if (minutes.Length > 1)
                formatString = formatString.Replace("MM", minutes);
            else
                formatString = formatString.Replace("MM", "0" + minutes);

            string seconds = timeSpan.Value.Seconds.ToString();
            if (seconds.Length > 1)
                formatString = formatString.Replace("SS", seconds);
            else
                formatString = formatString.Replace("SS", "0" + seconds);

            string millis = timeSpan.Value.Milliseconds.ToString();
            if (millis.Length > 2)
                formatString = formatString.Replace("ms", millis);
            else if (millis.Length > 1)
                formatString = formatString.Replace("ms", "0" + millis);
            else
                formatString = formatString.Replace("ms", "00" + millis);

            string micros = timeSpan.Value.Microseconds.ToString();
            if (micros.Length > 2)
                formatString = formatString.Replace("mcs", micros);
            else if (micros.Length > 1)
                formatString = formatString.Replace("mcs", "0" + micros);
            else
                formatString = formatString.Replace("mcs", "00" + micros);

            return formatString;
        }

    }
}
