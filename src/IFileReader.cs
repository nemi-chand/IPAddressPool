using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IPAddressPool
{
    public interface IFileReader
    {
		IList<string> Read(Stream stream);

		IList<string> Read(string filePath);
	}
}
