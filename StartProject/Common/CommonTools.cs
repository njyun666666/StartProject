using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Common
{
    public class CommonTools
    {
        public static int GetTimeStamp()
        {
            return GetTimeStamp(DateTime.UtcNow.ToLocalTime());
        }

        public static string GetFormatTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static int GetTimeStamp(DateTime value)
        {
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return ((int)span.TotalSeconds);
        }
        public static string Userip_Get(HttpContext context)
        {
            string IP = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
                {
                    IP = context.Request.Headers["X-Forwarded-For"];
                }
                else if (!string.IsNullOrEmpty(context.Request.Headers["MS_HttpContext"]))
                {
                    IP = context.Request.Headers["MS_HttpContext"];
                }
                else
                {
                    IP = context.Connection.RemoteIpAddress.ToString();

                    if (context.Connection.RemoteIpAddress.IsIPv4MappedToIPv6)
                    {
                        IP = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
                    }
                }

                if (!string.IsNullOrWhiteSpace(IP))
                {
                    string[] forwarded_ip_list = IP.ToString().Split(",");
                    IP = forwarded_ip_list.Length > 0 ? forwarded_ip_list[0] : "";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //System.Net.IPHostEntry ips = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            //IP = ips.AddressList.ToList().Where(p => p.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();
            return IP;
        }

    }
}
