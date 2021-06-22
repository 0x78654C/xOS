using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Core
{
    public class Network
    {


        //declare date variable
        private static string date = string.Empty;
        //------------------


        /// <summary>
        /// Verifies if IP is up or not
        /// </summary>
        /// <param name="ip">Enter the hostname/IP address.</param>
        /// <returns>string</returns>
       
        public static bool pingH(string ip)
        {
            bool pingable = false;
            Ping pinger = null;
            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(ip);
                pingable = reply.Status == IPStatus.Success;
                Thread.Sleep(200);

            }
            catch (PingException p)
            {
                date = DateTime.Now.ToString("yyyy-dd-HH:mm");
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }

            }
            return pingable;

        }
        /// <summary>
        /// Checking internet connection with Google DNS 8.8.8.8
        /// </summary>
        /// <returns>bool</returns>
        public static bool inetCK()
        {
            if (pingH("8.8.8.8"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///Check TCP connection for a specific address/hostname and port
        /// </summary>
        /// <param name="address">Add the ip/hostname address</param>
        /// <param name="port">Port</param>
        /// <returns>bool</returns>
        public static bool portCheck(string address,int port)
        {
            var connect = new TcpClient(address, port);
            if (connect.Connected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
