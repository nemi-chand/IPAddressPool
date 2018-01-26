using System;
using System.Collections.Generic;
using System.Text;

namespace IPAddressPool
{
    public static class FileReaderFactory
    {
		public static IFileReader CreateReader(string extension)
		{			
			switch (extension.ToLower())
			{
				case ".txt":
					return new TextFileReader();

				case ".json":
					return new JsonFileReader();
				default:
					throw new ArgumentException("'"+extension+"' the file extension not supported");						
			}
		}
    }
}
