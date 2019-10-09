using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Services
{
	public static class RequestHandler
	{
		static async Task<string> CallHttp(string url, HttpClient client = null)
		{
			if (client == null)
				client = new HttpClient();
			string result = await client.GetStringAsync(url);
			
			return result;
		}

		/// <summary>
		/// Sends a request.
		/// </summary>
		/// <param name="url">The request's URL.</param>
		/// <returns>The result of the response.</returns>
		public static string SendRequest(string url)
		{
			string result = "";
			try
			{
				Task<string> callTask = Task.Run(() => RequestHandler.CallHttp(url));
				callTask.Wait();
				result = callTask.Result;
				Console.WriteLine(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.Message);
			}
			return result;
		}

		/// <summary>
		/// Sends multiple requests.
		/// </summary>
		/// <param name="urls">The URLs of the requests to be sent.</param>
		/// <returns>A list of the results of the successful requests or null if the request was unsuccessful</returns>
		public static List<string> SendRequests(IEnumerable<string> urls)
		{
			List<string> results = new List<string>();
			HttpClient client = new HttpClient();
			try
			{
				List<Task<string>> tasks = new List<Task<string>>();
				foreach(string url in urls)
				{
					tasks.Add(Task.Run(() => RequestHandler.CallHttp(url, client)));
				}

				foreach(var task in tasks)
				{
					try
					{
						results.Add(task.Result);
					}
					catch(Exception e)
					{
						results.Add(null);
						Console.WriteLine("Exception: " + e.Message);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.Message);
			}
			client.Dispose();
			return results;
		}
	}
}
