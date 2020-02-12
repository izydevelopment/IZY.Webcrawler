using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Webcrawler.Tools.Stopwords
{
	public class StopwordList
	{
		public List<string> GetGermanStopwords()
		{
			var result = new List<string>();

			string path = GetFilePath(Directory.GetCurrentDirectory(), "Stopwords", "GermanStopwords.txt");

			string content = File.ReadAllText(path);

			result = content.Split(',').Select(x => x.Trim()).ToList();

			result.Add("y");

			return result;
		}
		private string GetFilePath(string path, string folderName, string fileName)
		{
			path = path.Replace(@"bin\Debug\netcoreapp2.1", String.Empty);
			path = path.Replace(".ConsoleApp", ".Tools");
			path += folderName + @"\";
			path += fileName;
			return path;
		}
	}
}
