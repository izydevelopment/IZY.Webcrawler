﻿using System.Collections.Generic;
using Webcrawler.Google;
using Webcrawler.Tools;
using Webcrawler.Tools.Models;
using Webcrawler.Tools.Analyzer;
using log4net.Config;
using System.IO;
using log4net;
using System.Reflection;

namespace Webcrawler.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var serpClient = new GoogleSerpClient();
			var serpResult = serpClient.Get("kuchen", 100);

			var websites = new List<HtmlPage>();

			foreach (var link in serpResult.Links)
			{
				var webClient = new AdvancedWebClient();
				var content = webClient.Get(link.UrlAsString);

				var htmlAnalyzer = new HtmlAnalyzer();
				var website = htmlAnalyzer.AnalyzeHtmlPage(content.HtmlContent);
				websites.Add(website);
			}

			var textAnalyzer = new TextAnalyzer();

			foreach (var website in websites)
			{
				var words = textAnalyzer.GetWords(website.Paragraphs, true);
			}
		}
	}
}
