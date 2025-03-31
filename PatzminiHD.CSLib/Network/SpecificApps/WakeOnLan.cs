using System;
using PatzminiHD.CSLib.ProgramInterfaces;

namespace PatzminiHD.CSLib.Network.SpecificApps
{
    /// <summary>
    /// Contains methods for sending a Wake-on-Lan package
    /// </summary>
    public static class WakeOnLan
    {
        /// <summary>
        /// Send a Wake-on-Lan package to a mac address
        /// </summary>
        /// <param name="macAddress">The MAC-Address of the device</param>
        /// <returns>null if the request was sent successfully, otherwise the exception that occured</returns>
        public static Exception? Send(string macAddress)
        {
            if(!Generic.ProgramExists("wakeonlan"))
                return new FileNotFoundException("The 'wakeonlan' package is not installed");
            
            try
            {
                Generic.StartProcess("wakeonlan", macAddress);
                return null;
            }
            catch(Exception e)
            {
                return e;
            }
        }
    }
}