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
        /// <param name="redirectOutput">Wether to redirect the standard output</param>
        /// <returns>The process output if useShellExecute is false and redirectStandardOutput is true</returns>
        public static (string? output, string? errorOutput) StartProcess(string fileName, string arguments, bool useShellExecute = false, bool redirectOutput = true)
        {
            string? output = null, errorOutput = null;
            if(redirectOutput && useShellExecute)
                throw new ArgumentException("useShellExecute has to be false to redirect the standard output");
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                UseShellExecute = useShellExecute,
                RedirectStandardOutput = redirectOutput,
                RedirectStandardError = redirectOutput,
                FileName = fileName,
                Arguments = arguments,
            };

            Process proc = new Process();
            proc.StartInfo = startInfo;
            proc.Start();
            if(!useShellExecute && redirectOutput)
            {
                //Read process output
                output = proc.StandardOutput.ReadToEnd();
                errorOutput = proc.StandardError.ReadToEnd();
            }
            proc.WaitForExit();
            proc.Dispose();
            
            return (output, errorOutput);
        }
    }
}
