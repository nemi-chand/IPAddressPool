using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace IPAddressPool
{
	public class IPPoolHelper
    {
		private static string _defaultregion = "IPAddressPool.DefaultRegion";
        private static IIPAddressPool instance;
		private static bool isBootstraped = false;
		//private static IList<KeyValuePair<string, string>> _poolList = new List<KeyValuePair<string, string>>();
		private static Dictionary<string, string> _poolList = new Dictionary<string, string>();

		public static IIPAddressPool Current
        {
            get
            {
                if(instance == null)
                {
                    instance = new DefaultIPAddressPool(_poolList,isBootstraped);
                }
                return instance;
            }
        }

		/// <summary>
		/// boot strap the ip helper first 
		/// </summary>
		/// <param name="filePath"></param>
		public static void Initialize(string filePath)
		{
			//validate input
			if(string.IsNullOrWhiteSpace(filePath))
			{
				throw new ArgumentException("filepath must not be empty.");
			}

			//_fileName = filePath;
			_poolList.Add(_defaultregion, filePath);
			isBootstraped = true;
		}

		/// <summary>
		/// boot strap the ip helper first 
		/// </summary>
		/// <param name="filePath"></param>
		public static void Initialize(string filePath,string regionName)
		{
			//validate input
			if (string.IsNullOrWhiteSpace(filePath))
			{
				throw new ArgumentException("filepath must not be empty.");
			}

			if (string.IsNullOrWhiteSpace(regionName))
			{
				throw new ArgumentException("Region must not be empty.");
			}

			//_fileName = filePath;
			_poolList.Add(regionName, filePath);
			isBootstraped = true;
		}

		/// <summary>
		/// boot strap the ip helper first 
		/// </summary>
		/// <param name="filePath"></param>
		public static void Initialize(Dictionary<string,string> regionList)
		{
			//validate input
			if (regionList.Count == 0)
			{
				throw new ArgumentException("list cannot be empty.");
			}

			_poolList = regionList;
			isBootstraped = true;
		}

		/// <summary>
		/// boot strap the ip helper first 
		/// </summary>
		/// <param name="filePath"></param>
		public static void Initialize(string[] ipPoolsArray, string regionName)
		{
			//validate input
			if (ipPoolsArray.Length == 0)
			{
				throw new ArgumentException("array cannot be empty.");
			}

			//_poolList = keyValuePairs;
			isBootstraped = true;
		}

		/// <summary>
		/// boot strap the ip helper first 
		/// </summary>
		/// <param name="filePath"></param>
		public static void Initialize(List<string> ipPoolsList, string regionName)
		{
			//validate input
			if (ipPoolsList.Count == 0)
			{
				throw new ArgumentException("list cannot be empty.");
			}

			//_poolList = keyValuePairs;
			isBootstraped = true;
		}
	}
}