using System;
using System.Collections.Generic;
using System.Text;

namespace Webcrawler.Google.Models
{
	public class SerpLink
	{
		private static int Count { get; set; } = 1;
		public int Position { get; set; }
		public string UrlAsString { get; set; }
		public Uri Url { get; set; }

		public SerpLink(string url)
		{
			this.Position = Count++;
			this.UrlAsString = url;
			this.Url = new Uri(url);
		}
	}
}
