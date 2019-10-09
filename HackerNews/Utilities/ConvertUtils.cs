using HackerNews.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace HackerNews.Utilities
{
	public static class ConvertUtils
	{
		/// <summary>
		/// Serializes objects to JSON.
		/// </summary>
		/// <param name="obj">The object to be serialized.</param>
		/// <param name="formatted">Optional paramater for the JSON's properties to be formatted in lowercase.</param>
		/// <returns>The JSON representation of the input object.</returns>
		public static string GetJson(object obj, bool formatted = false)
		{
			var serializerSettings = new JsonSerializerSettings();
			serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			string result = JsonConvert.SerializeObject(obj, serializerSettings);

			if (formatted)
			{
				result = JValue.Parse(result).ToString(Formatting.Indented);
			}
			return result;
		}

		/// <summary>
		/// Deserializes JSON to a Post object.
		/// </summary>
		/// <param name="message">The JSON representation of the Post.</param>
		/// <returns>The Post object or null in case it could not be deserialized.</returns>
		public static Post GetPost(string message)
		{
			Post result = null;
			try
			{
				result = JsonConvert.DeserializeObject<Post>(message);
			}
			catch(Exception e)
			{
				Console.Write(e.Message);
			}
			return result;
		}
	}
}
