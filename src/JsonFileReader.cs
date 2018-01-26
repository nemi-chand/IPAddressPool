using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IPAddressPool
{
	public class JsonFileReader : IFileReader
	{
		public IList<string> Read(Stream stream)
		{
			throw new NotImplementedException();
		}

		public IList<string> Read(string filePath)
		{
			return JsonConvert.DeserializeObject<IList<string>>(File.ReadAllText(filePath));
		}
	}
}
