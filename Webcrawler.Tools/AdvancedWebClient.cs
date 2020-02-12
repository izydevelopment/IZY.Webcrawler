using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Webcrawler.Tools
{
	public class AdvancedWebClient
	{
		private WebClient WebClient { get; set; } = new WebClient();
		public AdvancedWebClient()
		{
			this.WebClient.Encoding = System.Text.Encoding.UTF8;
			this.WebClient.Headers.Add("Content-Type", "text/html; charset=ISO-8859-1");
			//WebClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36");
		}

		/// <summary>
		/// Analysiert eine URL und liefert ein Ergebnis mit relevanten oberflächigen Informationen
		/// </summary>
		/// <param name="url">Die zu übergebende URL als String</param>
		/// <returns></returns>
		public AdvancedWebClientResult Get(string url)
		{
			var result = new AdvancedWebClientResult();

			if (IsValidUrl(url))
			{
				result.IsValidUrl = true;
				result.HtmlContent = DownloadContent(url);
				result.IsContentLoaded = true;
				result.HtmlContentLength = result.HtmlContent.Length;
				result.Encoding = WebClient.Encoding;
			}

			return result;
		}
		private string DownloadContent(string url)
		{
			string result = "";

			try
			{
				result = WebClient.DownloadString(url);
			}
			catch (WebException e)
			{
				var statusCode = e.Status;
			}
			catch (Exception)
			{

			}

			return result;
		}
		private static bool IsValidUrl(string url)
		{
			bool tryCreateResult = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult);
			if (tryCreateResult == true && uriResult != null)
				return true;
			else
				return false;
		}
	}
}
