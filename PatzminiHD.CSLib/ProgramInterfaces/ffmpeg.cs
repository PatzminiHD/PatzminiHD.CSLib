

namespace PatzminiHD.CSLib.ProgramInterfaces
{
    /// <summary>
    /// Contains Methods for interacting with FFmpeg
    /// </summary>
    public class FFmpeg
    {
        //ffmpeg -i <input_file> -c:v libsvtav1 -crf <quality> -cpu-used <num> -row-mt <num> <output_file>
        /// <summary>
        /// Reencode a video file to AV1 using ffmpeg/libsvtav1
        /// </summary>
        /// <param name="filename">The file you want to reencode</param>
        /// <param name="newFileName">The output filepath</param>
        /// <param name="quality">The quality ("crf" parameter) of the encoding:<br/>
        /// The CRF value can be from 0–63. Lower values mean better quality and greater file size. 0 means lossless. A CRF value of 23 yields a quality level corresponding to CRF 19 for x264, which would be considered visually lossless.</param>
        /// <param name="cpu_used">The cpu-used parameter of ffmpeg<br/>
        /// -cpu-used sets how efficient the compression will be. Default is 1. Lower values mean slower encoding with better quality, and vice-versa. Valid values are from 0 to 8 inclusive</param>
        /// <param name="row_mt">True to enable multi-threading<br/>
        /// -row-mt 1 enables row-based multi-threading which maximizes CPU usage. To enable fast decoding performance, also add tiles (i.e. -tiles 4x1 or -tiles 2x2 for 4 tiles). Enabling row-mt is only faster when the CPU has more threads than the number of encoded tiles</param>
        /// <param name="redirectOutputToConsole">True when you want to redirect the ffmpeg output to the console; otherwise false</param>
        /// <returns></returns>
        public static Exception? ReencodeVideo(string filename, string newFileName, uint quality, uint cpu_used = 1, bool row_mt = false, bool redirectOutputToConsole = true)
        {
            if (Environment.Get.OS != Environment.Get.OperatingSystem.Linux)
                return new NotSupportedException("FFmpeg is currently only supported on linux");

            if (quality > 63) //Value can only range from 0 to 63 (inclusive)
                return new ArgumentException("quality can only range from 0 to 63");

            if(cpu_used > 8) //Value can only range from 0 to 8 (inclusive)
                return new ArgumentException("cpu_used can only range from 0 to 8");

            if(!File.Exists(filename))
                return new ArgumentException("File to encode does not exist");

            if (File.Exists(newFileName))
                return new ArgumentException("Output file already exists");

            string arguments = $"-i \"{filename}\" -c:v libsvtav1 -crf {quality} -cpu-used {cpu_used} ";
            if (row_mt)
                arguments += $"-row-mt 1 ";
            arguments += $"\"{newFileName}\"";

            var result = Generic.StartProcess("ffmpeg", arguments, false, true, redirectOutputToConsole);
            if(result.output != null && result.output.Contains("[q] command received"))
            {
                return new Exception("Encoding has been interrupted by user");
            }
            else if(result.output != null && result.output.Contains("Exiting normally, received signal 2."))
            {
                return new Exception("ffmpeg received Ctrl-C");
            }
            else if(result.output != null && result.output.Contains("Conversion failed!"))
            {
                return new Exception("Conversion failed!");
            }
            return null;
        }
    }
}
