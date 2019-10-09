using System.Collections.Generic;
using HackerNews.Domain;
using HackerNews.IRepositories;
using HackerNews.Services;
using HackerNews.Utilities;

namespace HackerNews.Repositories
{
	class PostRepository : IPostRepository
	{
		public string[] GetTopPostsIds()
		{
			string idsJson = RequestHandler.SendRequest(GetGetPostIDsURL());
			return idsJson.TrimStart(new char[] { '[', ' ' }).TrimEnd(new char[] { '\n', ']', ' ' }).Split(", ");
		}

		public List<Post> GetPostsByIds(IEnumerable<string> ids)
		{
			List<Post> posts = new List<Post>();
			List<string> urls = new List<string>();

			foreach (string id in ids)
			{
				urls.Add(GetGetPostDetailsURL(id));
			}

			List<string> postsJson = RequestHandler.SendRequests(urls);
			foreach (var json in postsJson)
			{
				Post post = ConvertUtils.GetPost(json);
				if (post != null)
					posts.Add(post);
			}
			return posts;
		}

		public Post GetPostById(string id)
		{
			string postJson = RequestHandler.SendRequest(GetGetPostDetailsURL(id));
			return ConvertUtils.GetPost(postJson);
		}

		private string GetGetPostIDsURL()
		{
			return "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty";
		}

		private string GetGetPostDetailsURL(string id)
		{
			return $"https://hacker-news.firebaseio.com/v0/item/{id}.json?print=pretty";
		}

	}
}
