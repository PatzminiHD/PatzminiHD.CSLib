using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.ProgramInterfaces
{
    /// <summary>
    /// Contains Methods to start and interact with processes
    /// </summary>
    public class Generic
    {
        /// <summary>
        /// Start a process
        /// </summary>
        /// <param name="fileName">The name of the process</param>
        /// <param name="arguments">The arguments that should be given to the process</param>
        /// <param name="useShellExecute">Wether to use shell execute</param>
        /// <param name="redirectStandardOutput">Wether to redirect the standard output</param>
        public static void StartProcess(string fileName, string arguments, bool useShellExecute = false, bool redirectStandardOutput = true)
        {
            if(redirectStandardOutput && useShellExecute)
                throw new ArgumentException("useShellExecute has to be false to redirect the standard output");
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                UseShellExecute = useShellExecute,
                RedirectStandardOutput = redirectStandardOutput,
                FileName = fileName,
                Arguments = arguments,
            };

            Process proc = new Process();
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
            proc.Dispose();
        }
    }
}
