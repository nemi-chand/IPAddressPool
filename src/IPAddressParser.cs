using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IPAddressPool
{
    internal class IPAddressParser
    {
		/// <summary>
		/// this helps to parse the given ip address in form of xxx.xxx.xxx.0/xx, xxx.xxx.0.0/xx, xxx.0.0.0/xx
		/// </summary>
		/// <param name="ipaddress"></param>
		/// <returns></returns>
		public static string Parse(string ipaddress)
		{
			if(ipaddress.Contains("/"))
			{
				return ipaddress;
			}

			int starCount = ipaddress.Count(x => x == '*');
			switch (starCount)
			{
				case 1:
					return ipaddress.Replace("*", "0") + "/24";
				case 2:
					return ipaddress.Replace("*", "0") + "/16";
				case 3:
					return ipaddress.Replace("*", "0") + "/8";
				case 4:
					throw new ArgumentException("'"+ipaddress+"' IP address is not supported.");
			}
			return string.Empty;
		}
    }
}
