using System;
using System.Collections.Generic;
using System.Text;

namespace Webcrawler.Tools.Models
{
	public class HtmlPage
	{
		public string ContentAsString { get; set; }
		public Dictionary<int, List<string>> Headlines { get; set; } = new Dictionary<int, List<string>>();
		public List<string> Italics { get; set; }
		public List<string> Bolds { get; set; }
		public List<string> Paragraphs { get; set; }
		public List<string> Links { get; set; } = new List<string>();
	}
}
