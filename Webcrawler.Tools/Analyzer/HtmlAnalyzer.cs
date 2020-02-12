using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webcrawler.Tools.Models;

namespace Webcrawler.Tools.Analyzer
{
	public class HtmlAnalyzer
	{
		private HtmlDocument HtmlDocument { get; set; } = new HtmlDocument();

		/// <summary>
		/// Analysiert eine HTML Website und liefert alle relevanten Informationen zum Aufbau der Seite
		/// </summary>
		/// <param name="contentAsString">Der HTML Content als String</param>
		/// <returns></returns>
		public HtmlPage AnalyzeHtmlPage(string contentAsString)
		{
			var result = new HtmlPage();

			try
			{
				this.HtmlDocument.LoadHtml(contentAsString);
				result.ContentAsString = contentAsString;
				result.Links = GetLinks();
				result.Headlines = GetHeadlines();
				result.Paragraphs = GetParagraps();
				result.Italics = GetItalics();
				result.Bolds = GetBolds();
			}
			catch (Exception)
			{
				return result;
			}

			return result;
		}
		private List<string> GetLinks()
		{
			var result = new List<string>();

			foreach (HtmlNode link in this.HtmlDocument.DocumentNode.SelectNodes("//a[@href]"))
			{
				string hrefValue = link.GetAttributeValue("href", string.Empty);

				if (!result.Contains(hrefValue))
				{
					result.Add(hrefValue);
				}
			}

			return result;
		}
		private Dictionary<int, List<string>> GetHeadlines()
		{
			// TODO : Refactoring der wiederholenden Anweisungen

			var result = new Dictionary<int, List<string>>();

			var h1Nodes = GetHtmlNodes("//h1");

			if (h1Nodes != null)
			{
				result.Add(1, new List<string>());

				foreach (HtmlNode h1 in h1Nodes)
				{
					string h1Value = h1.InnerHtml;

					result.Where(x => x.Key == 1).FirstOrDefault().Value.Add(h1Value);
				}
			}

			var h2Nodes = GetHtmlNodes("//h2");

			if (h2Nodes != null)
			{
				result.Add(2, new List<string>());

				foreach (HtmlNode h2 in h2Nodes)
				{
					string h2Value = h2.InnerHtml;

					result.Where(x => x.Key == 2).FirstOrDefault().Value.Add(h2Value);
				}
			}

			var h3Nodes = GetHtmlNodes("//h3");

			if (h3Nodes != null)
			{
				result.Add(3, new List<string>());

				foreach (HtmlNode h3 in h3Nodes)
				{
					string h3Value = h3.InnerHtml;


					result.Where(x => x.Key == 3).FirstOrDefault().Value.Add(h3Value);
				}
			}

			return result;
		}
		private List<string> GetParagraps()
		{
			var result = new List<string>();

			var paragraphNodes = GetHtmlNodes("//p");

			if (paragraphNodes != null)
			{
				foreach (HtmlNode paragraphNode in paragraphNodes)
				{
					string paragraphValue = paragraphNode.InnerHtml;
					result.Add(paragraphValue);
				}
			}

			return result;
		}
		private List<string> GetItalics()
		{
			var result = new List<string>();

			var italicNodes = GetHtmlNodes("//i");

			if (italicNodes != null)
			{
				foreach (HtmlNode italicNode in italicNodes)
				{
					string italicValue = italicNode.InnerHtml;
					result.Add(italicValue);
				}
			}

			return result;
		}
		private List<string> GetBolds()
		{
			var result = new List<string>();

			var boldNodes = GetHtmlNodes("//i");

			if (boldNodes != null)
			{
				foreach (HtmlNode boldNode in boldNodes)
				{
					string boldValue = boldNode.InnerHtml;
					result.Add(boldValue);
				}
			}

			return result;
		}
		private HtmlNodeCollection GetHtmlNodes(string xpath)
		{
			try
			{
				var result = this.HtmlDocument.DocumentNode.SelectNodes(xpath);
				return result;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
