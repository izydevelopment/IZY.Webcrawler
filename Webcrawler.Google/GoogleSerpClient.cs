using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using Webcrawler.Google.Models;
using Webcrawler.Tools;

namespace Webcrawler.Google
{
	/// <summary>
	/// Zur ermittlung einer SERP die Get Methode aufgrufen
	/// </summary>
	public class GoogleSerpClient
	{
		private HtmlDocument HtmlDocument { get; set; } = new HtmlDocument();

		/// <summary>
		/// Analysiert die Suchergebnisse der Google SERP und liefert ein Ergebniss mit den rankenden Seiten
		/// </summary>
		/// <param name="searchTerm">Der Suchbegriff mithilfe die zugehörige SERP ermittelt werden soll</param>
		/// <param name="snippetCount">Die Anzahl der Suchergebnisse die berücksichtigt werden sollen</param>
		/// <returns></returns>
		public GoogleSerpClientResult Get(string searchTerm, int snippetCount = 20)
		{
			var googleSearchQuery = BuildGoogleSearchQuery(searchTerm, snippetCount);
			var contentAsString = MakeGoogleRequest(googleSearchQuery);
			var result = ParseGoogleRequestResult(contentAsString, snippetCount);

			result.SearchTerm = searchTerm;

			return result;
		}
		private protected string BuildGoogleSearchQuery(string searchTerm, int snippetCount)
		{
			searchTerm = searchTerm.Replace(" ", "+");
			return String.Format("http://www.google.com/search?q={0}&num={1}", searchTerm, snippetCount);
		}
		private string MakeGoogleRequest(string googleSearchQuery)
		{
			var webClient = new AdvancedWebClient();
			var result = webClient.Get(googleSearchQuery);
			return result.HtmlContent;
		}
		private GoogleSerpClientResult ParseGoogleRequestResult(string contentAsString, int snippetCount = 20)
		{
			var result = new GoogleSerpClientResult();

			this.HtmlDocument.LoadHtml(contentAsString);

			var selectNodes = this.HtmlDocument.DocumentNode.SelectNodes("//div[@class='kCrYT']");

			foreach (var node in selectNodes)
			{
				if (node.InnerHtml.Contains("url?q=") && node.InnerHtml.StartsWith("<a href") && node.InnerHtml.Contains("&amp;sa=U&amp;") && !node.InnerHtml.Contains("youtube"))
				{
					var firstIndexTerm = "url?q=";
					var lastIndexTerm = "&amp;sa=U&amp;";

					var firstIndex = node.InnerHtml.IndexOf(firstIndexTerm) + firstIndexTerm.Length;
					// wieso + 2?
					var lastIndex = node.InnerHtml.IndexOf(lastIndexTerm) - (lastIndexTerm.Length + 2);

					if(result.Links.Count >= snippetCount)
					{
						return result;
					}

					result.Links.Add(new SerpLink(node.InnerHtml.Substring(firstIndex, lastIndex)));
				}
			}

			return result;
		}
	}
}
