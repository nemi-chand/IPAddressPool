using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

namespace IPAddressPool
{
	public class DefaultIPAddressPool : IIPAddressPool
	{
				
		private static Dictionary<string, IList<IPAddress>> availableSubnetMasks = new Dictionary<string, IList<IPAddress>>();
		private static Dictionary<string, IDictionary<IPAddress, string>> networkAddresses = new Dictionary<string, IDictionary<IPAddress, string>>();
		private bool isBootstraped = false;
		private static bool isInitialized = false;
		private Dictionary<string, string> _poolList = new Dictionary<string, string>();

		public IList<string> RegionKeys => (from kvp in _poolList select kvp.Key).ToList();

		public DefaultIPAddressPool(Dictionary<string, string> ipranges, bool isBootstraped)
		{
			this._poolList = ipranges;
			this.isBootstraped = isBootstraped;
		}

		private void Initialize()
		{
			//make sure the ip helper is bootstraped
			if (!isBootstraped)
			{
				throw new ArgumentException("IP Pool Helper is not Initialize.");
			}

			//iterate the pool list
			foreach (KeyValuePair<string, string> item in _poolList)
			{
				//get the extension
				string extension = Path.GetExtension(item.Value);
				//file reader instance
				IFileReader fileReader = FileReaderFactory.CreateReader(extension);
				//load the ip pool
				IList<string> ipPools = fileReader.Read(item.Value);
				foreach (var op in ipPools)
				{
					string formatedIP = IPAddressParser.Parse(op);
					string[] poolArr = formatedIP.Trim().Split('/');
					if (poolArr.Length > 0)
					{
						int subnetMast;
						Int32.TryParse(poolArr[1], out subnetMast);
						string poolIp = poolArr[0].Replace(" ", "");
						IPAddress ipAddr = IPAddress.Parse(poolIp);
						IPAddress mask = IPAddress.Parse(CIDRDecimalConvertor.CIDR2Decimal(subnetMast));
						AddSubnetMaskToDictonary(item.Key, mask);

						IPAddress netAddOfIP = ipAddr.GetNetworkAddress(mask);
						AddNetAddressToDictonary(item.Key, netAddOfIP, op);
					}
				}
			}
			//set the initialize flag to true
			isInitialized = true;
		}

		private void AddSubnetMaskToDictonary(string region, IPAddress mask)
		{
			if (availableSubnetMasks.ContainsKey(region))
			{
				if (!availableSubnetMasks[region].Contains(mask))
				{
					availableSubnetMasks[region].Add(mask);
				}
			}
			else
			{
				availableSubnetMasks.Add(region, new List<IPAddress>() { mask });
			}
		}

		private void AddNetAddressToDictonary(string region, IPAddress netAddressOfIPAddress, string ipRange)
		{
			if (networkAddresses.ContainsKey(region))
			{
				if (!networkAddresses[region].ContainsKey(netAddressOfIPAddress))
				{
					networkAddresses[region].Add(netAddressOfIPAddress, ipRange);
				}
			}
			else
			{
				networkAddresses.Add(region,
						 new Dictionary<IPAddress, string>()
						 {
							{
								netAddressOfIPAddress,
								ipRange
							}
						 });
			}
		}

		public string CIDR2Decimal(int cidr)
		{
			return CIDRDecimalConvertor.CIDR2Decimal(cidr);
		}

		public IPAddress GetNetworkAddress(IPAddress address, IPAddress subnetMask)
		{
			return address.GetNetworkAddress(subnetMask);
		}

		public bool IsExists(IPAddress Ipaddress)
		{
			if (!isInitialized)
				Initialize();

			foreach (KeyValuePair<string, IList<IPAddress>> item in availableSubnetMasks)
			{
				//iterate the available list of subnet masts
				foreach (IPAddress currentmask in availableSubnetMasks[item.Key])
				{
					//calculate the network address of current ip 
					IPAddress netAddOfCurrentIP = Ipaddress.GetNetworkAddress(currentmask);
					//check if the same network address contain the dictionary 
					if (networkAddresses[item.Key].ContainsKey(netAddOfCurrentIP))
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool IsExists(string Ipaddress)
		{
			IPAddress validiPAddress = ValidateIPAddress(Ipaddress);

			return IsExists(validiPAddress);
		}

		public bool IsExists(IPAddress Ipaddress, string region)
		{
			if (!isInitialized)
				Initialize();

			//make sure subnet mask contain the region

			if (!availableSubnetMasks.ContainsKey(region))
			{
				throw new ArgumentException("'" + region + "' region doesn't exists. Make sure region is correct.");
			}

			//iterate the available list of subnet masts
			foreach (IPAddress currentmask in availableSubnetMasks[region])
			{
				//calculate the network address of current ip 
				IPAddress netAddOfCurrentIP = Ipaddress.GetNetworkAddress(currentmask);
				//check if the same network address contain the dictionary 
				if (networkAddresses[region].ContainsKey(netAddOfCurrentIP))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsExists(string Ipaddress, string region)
		{
			IPAddress validiPAddress = ValidateIPAddress(Ipaddress);

			return IsExists(validiPAddress, region);
		}

		public bool IsInSameSubnet(IPAddress networkAddress1, IPAddress networkAddress2, IPAddress subnetMask)
		{
			return networkAddress2.IsInSameSubnet(networkAddress1, subnetMask);
		}

		public int Decimal2CIDR(string decimalMask)
		{
			return CIDRDecimalConvertor.Decimal2CIDR(decimalMask);
		}

		public int Decimal2CIDR(IPAddress decimalMask)
		{
			if (decimalMask != null)
				return 0;

			return CIDRDecimalConvertor.Decimal2CIDR(decimalMask.ToString());
		}

		public string GetRegion(IPAddress iPAddress)
		{
			if (!isInitialized)
				Initialize();

			foreach (KeyValuePair<string, IList<IPAddress>> item in availableSubnetMasks)
			{
				//iterate the available list of subnet masts
				foreach (IPAddress currentmask in availableSubnetMasks[item.Key])
				{
					//calculate the network address of current ip 
					IPAddress netAddOfCurrentIP = iPAddress.GetNetworkAddress(currentmask);
					//check if the same network address contain the dictionary 
					if (networkAddresses[item.Key].ContainsKey(netAddOfCurrentIP))
					{
						return item.Key;
					}
				}
			}
			return string.Empty;
		}

		public string GetRegion(string striPAddress)
		{
			IPAddress iPAddress = ValidateIPAddress(striPAddress);
			return GetRegion(iPAddress);
		}

		private IPAddress ValidateIPAddress(string strIPAddresss)
		{
			IPAddress validiPAddress;
			if (!IPAddress.TryParse(strIPAddresss, out validiPAddress))
			{
				throw new ArgumentException("Unable to parse given IPAddress '" + strIPAddresss + "'");
			}

			return validiPAddress;
		}
	}
}
