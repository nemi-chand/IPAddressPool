using System;
using System.Net;


namespace IPAddressPool
{
	public static class IPAddressExtensionHelper
    {
        /// <summary>
        /// Get network address by using ip address and subnetmask
        /// </summary>
        /// <param name="address">ip address</param>
        /// <param name="subnetMask">subnet mask address</param>
        /// <returns>(IPAddress)network ip address</returns>
        public static IPAddress GetNetworkAddress(this IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] & (subnetMaskBytes[i]));
            }
            return new IPAddress(broadcastAddress);
        }

        /// <summary>
        /// Validating IPAddress with network IPAddress and Network SubNet mask Address
        /// </summary>
        /// <param name="address2">IPAddress to be validate</param>
        /// <param name="address">Network IPAddress</param>
        /// <param name="subnetMask">Subnet Mask IPAddress</param>
        /// <returns></returns>
        public static bool IsInSameSubnet(this IPAddress address2, IPAddress address, IPAddress subnetMask)
        {
            IPAddress network1 = address.GetNetworkAddress(subnetMask);
            IPAddress network2 = address2.GetNetworkAddress(subnetMask);

            return network1.Equals(network2);
        }
	}
}