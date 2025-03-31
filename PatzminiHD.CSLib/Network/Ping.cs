using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace PatzminiHD.CSLib.Network
{
    /// <summary>
    /// Contains methods for sending pings
    /// </summary>
    public static class Ping
    {
        /// <summary>
        /// Send ping to an IP Address
        /// </summary>
        /// <param name="ipAddress">The IP Address of the device</param>
        /// <param name="timeout">The timeout in milliseconds</param>
        /// <returns>True if the device responded to the ping, otherwise false</returns>
        public static bool SendPing(string ipAddress, int timeout = 500)
        {
            System.Net.NetworkInformation.Ping pingSender = new();
            var reply = pingSender.Send(ipAddress, timeout);
            
            return reply.Status == IPStatus.Success;
        }
    }
}