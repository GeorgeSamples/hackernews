using HackerNews.Domain;
using System.Collections.Generic;

namespace HackerNews.IRepositories
{
	public interface IPostRepository
	{
		/// <summary>
		/// Gets the top posts IDs.
		/// </summary>
		/// <returns>A string array containing the top posts IDs</returns>
		string[] GetTopPostsIds();

		/// <summary>
		/// Gets the post with the given id.
		/// </summary>
		/// <param name="id">The id of the requested post.</param>
		/// <returns>The post having the input id if exists else null.</returns>
		Post GetPostById(string id);

		/// <summary>
		/// Gets the posts with the ids provided.
		/// </summary>
		/// <param name="ids">The ids of the requested posts.</param>
		/// <returns>A list of posts having the input id as id if exist else null.</returns>
		List<Post> GetPostsByIds(IEnumerable<string> ids);
	}
}
