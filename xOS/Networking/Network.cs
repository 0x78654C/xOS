using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace Core
{
    public class Network
    {


        //declare date variable
        private static string s_date = string.Empty;
        //------------------


        /// <summary>
        /// Verifies if IP is up or not
        /// </summary>
        /// <param name="ip">Enter the hostname/IP address.</param>
        /// <returns>string</returns>

        public static bool PingHost(string ip)
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
            catch
            {
                s_date = DateTime.Now.ToString("yyyy-dd-HH:mm");
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
        public static bool InternetCheck()
        {
            return PingHost("8.8.8.8");                           
        }

        /// <summary>
        ///Check TCP connection for a specific address/hostname and port
        /// </summary>
        /// <param name="address">Add the ip/hostname address</param>
        /// <param name="port">Port</param>
        /// <returns>bool</returns>
        public static bool PortCheck(string address, int port)
        {
            var connect = new TcpClient(address, port);
            return connect.Connected;
        }
    }
}
