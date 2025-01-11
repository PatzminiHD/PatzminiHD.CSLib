using System.IO.Compression;

namespace PatzminiHD.CSLib.FileSystem.Compression
{
    /// <summary>
    /// Contains Methods for Compressing and Decompressing gzipped streams and files
    /// </summary>
    public static class GZip
    {
        /// <summary>
        /// Decompress a gzipped stream
        /// </summary>
        /// <param name="stream">A gzip compressed stream</param>
        /// <returns>An ungzipped stream</returns>
        public static Stream Decompress(Stream stream)
        {
            return new GZipStream(stream, CompressionMode.Decompress);
        }

        /// <summary>
        /// Decompress a gzipped file to a new file
        /// </summary>
        /// <param name="filename">The path to the gzipped file</param>
        /// <param name="newFileName">The path of the new ungzipped file</param>
        /// <param name="overwrite">True if you want to overwrite a file if it exists at newFileName, otherwise false</param>
        /// <exception cref="FileNotFoundException">If the file in filename was not found</exception>
        /// <exception cref="Exception">If a file at newFileName already exists, but overwriting was not enabled</exception>
        public static void Decompress(string filename, string newFileName, bool overwrite = false)
        {
            if(!File.Exists(filename))
                throw new FileNotFoundException("The given file does not exist");
            using FileStream compressedFileStream = File.Open(filename, FileMode.Open);

            if(File.Exists(newFileName))
            {
                if (overwrite)
                    File.Delete(newFileName);
                else
                    throw new Exception("The given output file already exists and overwriting has not been enabled");
            }

            using FileStream outputFileStream = File.Create(newFileName);

            using var decompressedStream = Decompress(compressedFileStream);

            decompressedStream.CopyTo(outputFileStream);
        }
    }
}
