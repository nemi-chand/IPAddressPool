using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace IPAddressPool
{
    public interface IIPAddressPool
    {

		/// <summary>
		/// get the list of region keys
		/// </summary>
		IList<string> RegionKeys { get; }

		/// <summary>
		/// check if IPAddress exists in IP pools/ranges
		/// </summary>
		/// <param name="Ipaddress">IPAddress to check</param>
		/// <returns>bool</returns>
		bool IsExists(IPAddress Ipaddress);

		/// <summary>
		/// check if IPAddress exists in IP pools/ranges
		/// </summary>
		/// <param name="Ipaddress">string to check</param>
		/// <returns>bool</returns>
		bool IsExists(string Ipaddress);

		/// <summary>
		/// check if IPAddress exists in perticular region of IP pools
		/// </summary>
		/// <param name="Ipaddress">IPaddress to check</param>
		/// <param name="region">in which region to check the above IPAddress</param>
		/// <returns></returns>
		bool IsExists(IPAddress Ipaddress, string region);

		/// <summary>
		/// check if IPAddress exists in perticular region of IP pools
		/// </summary>
		/// <param name="Ipaddress">string to check</param>
		/// <param name="region">in which region to check the above IPAddress</param>
		/// <returns></returns>
		bool IsExists(string Ipaddress, string region);

		/// <summary>
		/// Get network address by using ip address and subnetmask
		/// </summary>
		/// <param name="address">ip address</param>
		/// <param name="subnetMask">subnet mask address</param>
		/// <returns>(IPAddress)network ip address</returns>
		IPAddress GetNetworkAddress(IPAddress address, IPAddress subnetMask);

		/// <summary>
		/// Validating IPAddress with network IPAddress and Network SubNet mask Address
		/// </summary>
		/// <param name="address2">IPAddress to be validate</param>
		/// <param name="address">Network IPAddress</param>
		/// <param name="subnetMask">Subnet Mask IPAddress</param>
		/// <returns></returns>
		bool IsInSameSubnet(IPAddress networkAddress2, IPAddress networkAddress, IPAddress subnetMask);

		/// <summary>
		/// Convert CIDR to Decimal like 15(CIDR) to 255.254.0.0(decimal)
		/// </summary>
		/// <param name="cidr">CIDR Number</param>
		/// <returns>mask ip decimal string</returns>
		string CIDR2Decimal(int cidr);

		/// <summary>
		/// Convert Decimal to CIDR like 255.254.0.0(decimal) to 15(CIDR)
		/// </summary>
		/// <param name="decimalMask">decimal mask to convert e.g. 255.254.0.0</param>
		/// <returns>int CIDR of given decimal subnet mask</returns>
		int Decimal2CIDR(string decimalMask);

		/// <summary>
		/// Convert Decimal to CIDR like 255.254.0.0(decimal) to 15(CIDR)
		/// </summary>
		/// <param name="decimalMask">decimal mask to convert e.g. 255.254.0.0</param>
		/// <returns>int CIDR of given decimal subnet mask</returns>
		int Decimal2CIDR(IPAddress decimalMask);

		/// <summary>
		/// get region name while having multiple region it helps to identify the region in which IPAddress does exist
		/// </summary>
		/// <param name="iPAddress"></param>
		/// <returns></returns>
		string GetRegion(IPAddress iPAddress);

		/// <summary>
		/// get region name while having multiple region it helps to identify the region in which IPAddress does exist
		/// </summary>
		/// <param name="iPAddress"></param>
		/// <returns></returns>
		string GetRegion(string iPAddress);

	}
}
