using System;
using System.Collections.Generic;
using System.Text;
using Webcrawler.Google.Models;

namespace Webcrawler.Google
{
	public class GoogleSerpClientResult
	{
		public string SearchTerm { get; set; }
		public List<SerpLink> Links { get; set; } = new List<SerpLink>();
	}
}
