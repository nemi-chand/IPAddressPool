using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace IPAddressPool.Test
{
    public class UnitTest1
    {
        [Fact]
        public void IsExistsTest()
        {
			BootstrapIPPoolWrapper();

			bool result = IPPoolHelper.Current.IsExists(IPAddress.Parse("41.157.0.128"), "region1"); //O(1) or log(n)

			bool result2 = IPPoolHelper.Current.IsExists("41.157.0.128/12");

			Assert.True(result);
			Assert.True(result2);
		}

		[Fact]
		public void GetRegionTest()
		{
			BootstrapIPPoolWrapper();

			string regionName = IPPoolHelper.Current.GetRegion("41.157.0.128");

			Assert.Equal(regionName, "region1");
		}
		
		public void BootstrapIPPoolWrapper()
		{
			//bootstarp the ip helper
			//IPPoolWrapper.IPPoolHelper.Initialize("IpList.json","idea");

			IPPoolHelper.Initialize(new Dictionary<string, string>()
			{
				{ "region1","test1.json"},
				{ "region2","test2.json"},
				{ "region3","test3.json"}
			});
		}
    }
}
