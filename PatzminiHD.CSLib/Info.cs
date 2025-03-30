using System.Globalization;
using System.Reflection;

namespace PatzminiHD.CSLib
{
    /// <summary>
    /// Contains Infos about the Library
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class Info : Attribute
    {
        /// <summary> The name of the Library </summary>
        public static string Name = "PatzminiHD.CSLib";
        /// <summary> The version of the Library </summary>
        public static string Version = "v2.26.0";

        /// <summary>
        /// Constructor used by the automatic assigning of attributes
        /// </summary>
        /// <param name="buildTimeStamp">The time when the Library was built</param>
        public Info(string buildTimeStamp)
        {
            BuildTime = DateTime.ParseExact(buildTimeStamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        /// <summary> BuildTime property method </summary>
        public DateTime BuildTime { get; set; }

        /// <summary> The DateTime when the Library was built </summary>
        public static DateTime BuildTimeStamp
        {
            get
            {
                var assembly = typeof(Info).Assembly;
                var attribute = assembly.GetCustomAttribute<Info>();

                return attribute != null ? attribute.BuildTime : DateTime.MinValue;
            }
        }
    }

    
}
