using System;
using System.Collections.Generic;
using System.Text;

namespace Webcrawler.Tools
{
	public class AdvancedWebClientResult
	{
		public bool IsValidUrl { get; set; } = false;
		public bool IsContentLoaded { get; set; } = false;
		public string HtmlContent { get; set; }
		public int HtmlContentLength { get; set; } = 0;
		public Encoding Encoding { get; set; }
	}
}
