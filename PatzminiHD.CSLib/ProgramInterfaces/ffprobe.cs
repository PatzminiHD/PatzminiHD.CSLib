namespace PatzminiHD.CSLib.ProgramInterfaces
{
    /// <summary>
    /// Contains Methods for interacting with FFprobe
    /// </summary>
    public class FFprobe
    {
        /// <summary>
        /// Check if a file is a video file
        /// </summary>
        /// <param name="filePath">The path of the file to check</param>
        /// <returns>True if the file is a video file, otherwise false</returns>
        public static bool IsVideoFile(string filePath)
        {
            if(!File.Exists(filePath))
                return false;

            var ffprobeOutput = Generic.StartProcess("ffprobe", $"-loglevel error -show_entries stream=codec_type -of default=nw=1 \"{filePath}\"", false, true, false).output;

            if(ffprobeOutput != null && ffprobeOutput.Contains("video"))
                return true;

            return false;
        }

        /// <summary>
        /// Get the codec name of a video file
        /// </summary>
        /// <param name="filePath">The path of the file to check</param>
        /// <returns>The codec of the video, null if the file is not a video file</returns>
        public static string? GetCodecName(string filePath)
        {
            if(!File.Exists(filePath))
                return null;

            return Generic.StartProcess("ffprobe", $"-v error -select_streams v:0 -show_entries stream=codec_name -of default=nokey=1:noprint_wrappers=1 \"{filePath}\"", false, true, false).output;
        }
    }
}
