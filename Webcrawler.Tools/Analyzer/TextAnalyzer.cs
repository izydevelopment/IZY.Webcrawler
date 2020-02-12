using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Webcrawler.Tools.Stopwords;

namespace Webcrawler.Tools.Analyzer
{
	public class TextAnalyzer
	{
		public string GetText(List<string> sentences)
		{
			string result = "";

			foreach (var sentence in sentences)
			{
				result += GetCleanContent(sentence);
			}

			return result;
		}
		public List<string> GetSentences(string content)
		{
			var result = new List<string>();

			var splittedWithDot = content.Split('.');

			foreach (var dotSentence in splittedWithDot)
			{
				var questionMarkSentences = dotSentence.Split('?');

				foreach (var questionMarkSentence in questionMarkSentences)
				{
					var excalamtionMarkSentences = questionMarkSentence.Split('!');

					foreach (var item in excalamtionMarkSentences)
					{
						var sentence = Regex.Replace(item, @"\s+", " ");

						if (item != "")
						{
							result.Add(sentence.Trim());
						}
					}
				}
			}

			return result;
		}

		public Dictionary<string, int> GetWords(List<string> sentences, bool removeHtmlTags = false)
		{
			var result = new Dictionary<string, int>();
			var stopwordList = new StopwordList().GetGermanStopwords();

			foreach (var sentence in sentences)
			{
				var temp = sentence;

				if (removeHtmlTags)
				{
					temp = RemoveHtmlTags(sentence);
				}

				var words = GetCleanContent(temp).Split(' ');


				foreach (var word in words)
				{
					if (string.IsNullOrEmpty(word) || stopwordList.Contains(word.ToLower()))
					{
						continue;
					}

					if (!result.Keys.Contains(word.ToLower()))
					{
						result.Add(word.ToLower(), 1);
					}
					else
					{
						result[word.ToLower()]++;
					}
				}
			};

			return result.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
		}

		public Dictionary<string, int> GetWords(string content)
		{
			var result = new Dictionary<string, int>();
			var stopwordList = new StopwordList().GetGermanStopwords();

			var words = content.Split(' ');

			foreach (var word in words)
			{
				if (string.IsNullOrEmpty(word) || stopwordList.Contains(word.ToLower()))
				{
					continue;
				}

				if (!result.Keys.Contains(word.ToLower()))
				{
					result.Add(word.ToLower(), 1);
				}
				else
				{
					result[word.ToLower()]++;
				}
			}

			return result.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
		}

		private protected string RemoveHtmlTags(string input)
		{
			return Regex.Replace(input, "<.*?>", " ");
		}

		/// <summary>
		/// Entfernt alle HTML Elemente und diverse Sonderzeichen
		/// </summary>
		/// <param name="content">Der zu bereinigende Inhalt</param>
		/// <param name="removePunctuation">Gibt an, ob alle Sonderzeichen entfernt werden sollen</param>
		/// <returns name ="result">Der bereinigte Inhalt</returns>
		private protected string GetCleanContent(string content, bool removePunctuation = false)
		{
			content = RemoveHtmlTags(content);
			content = System.Net.WebUtility.HtmlDecode(content);

			// Entferne diverse Sonderzeichen
			content = Regex.Replace(content, @"[^a-zA-Z äöüÄÖÜß\.\?\!]", " ");

			if (removePunctuation)
			{
				content = Regex.Replace(content, @"[\.\?!]", " ");
			}

			// Entferne mehrfache Leerzeichen durch eines
			content = Regex.Replace(content, @"\s+", " ");
			var result = content.Trim().ToLower();

			return result;
		}
	}
}
