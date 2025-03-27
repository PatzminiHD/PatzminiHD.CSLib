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
        /// <param name="redirectOutput">Wether to redirect the output to the return values of this method</param>
        /// <param name="redirectOutputToConsole">Wether to redirect the output to the console</param>
        /// <returns>The process output if useShellExecute is false and redirectStandardOutput is true</returns>
        public static (string? output, string? errorOutput) StartProcess(string fileName, string arguments, bool useShellExecute = false, bool redirectOutput = true, bool redirectOutputToConsole = false)
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

            var exitEventHandler = new EventHandler((object? sender, EventArgs args) => proc.Kill());
            AppDomain.CurrentDomain.ProcessExit += exitEventHandler;

            if(!useShellExecute && redirectOutput && redirectOutputToConsole)
            {
                proc.OutputDataReceived += (s, e) => { Console.WriteLine(e.Data); output += e.Data; };
                proc.ErrorDataReceived += (s, e) => { Console.WriteLine(e.Data); errorOutput += e.Data; };
            }
            else if(!useShellExecute && redirectOutput)
            {
                proc.OutputDataReceived += (s, e) => output += e.Data;
                proc.ErrorDataReceived += (s, e) => errorOutput += e.Data;
            }

            proc.Start();            
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();
            proc.Dispose();

            AppDomain.CurrentDomain.ProcessExit -= exitEventHandler;
            
            return (output, errorOutput);
        }
    }
}
