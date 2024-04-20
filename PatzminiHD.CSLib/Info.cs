using System.Globalization;
using System.Reflection;

namespace PatzminiHD.CSLib
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class Info : Attribute
    {
        public static string Name = "PatzminiHD.CSLib";
        public static string Version = "v0.1.0";
        public Info(string buildTimeStamp)
        {
            BuildTime = DateTime.ParseExact(buildTimeStamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public DateTime BuildTime { get; set; }

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
