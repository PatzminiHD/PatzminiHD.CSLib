using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.FileSystem
{
    /// <summary>
    /// Contains usefuls Methods for handling directories
    /// </summary>
    public class Directory
    {
        /// <summary>
        /// Get all files in a given base directory. Optionally specify how deep subdirectories should be searched
        /// </summary>
        /// <param name="baseDirectory">The path to the base directory</param>
        /// <param name="sublevels">How many sublevels deep to search. 0 means to only search for files in the base directory<br/>
        ///                         Any number less then 0 means no limit.
        /// </param>
        /// <returns>A List of all found files</returns>
        /// <exception cref="DirectoryNotFoundException">If a directory could not be found</exception>
        public static async Task<List<string>> GetAllFiles(string baseDirectory, int sublevels = -1)
        {
            List<string> files = new List<string>();
            if (!System.IO.Directory.Exists(baseDirectory))
                throw new DirectoryNotFoundException($"The give directory '{baseDirectory}' does not exist");

            files.AddRange(System.IO.Directory.GetFiles(baseDirectory));

            if(sublevels <= 0)
                return files;

            foreach (var directory in System.IO.Directory.GetDirectories(baseDirectory))
            {
                files.AddRange(await GetAllFiles(directory, sublevels - 1));
            }

            return files;
        }

        /// <summary>
        /// Get all files in a given base directory where the filename matches a regex. Optionally specify how deep subdirectories should be searched
        /// </summary>
        /// <param name="baseDirectory">The path to the base directory</param>
        /// <param name="rule">The regex to which the filenames have to adhere</param>
        /// <param name="sublevels">How many sublevels deep to search. 0 means to only search for files in the base directory<br/>
        ///                         Any number less then 0 means no limit.</param>
        /// <returns></returns>
        public static async Task<List<string>> GetAllFilesWhere(string baseDirectory, Regex rule, int sublevels = -1)
        {
            List<string> initialFiles = await GetAllFiles(baseDirectory, sublevels);

            initialFiles.RemoveAll(x => !rule.IsMatch(x));

            return initialFiles;
        }
    }
}
