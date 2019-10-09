using HackerNews.Domain;
using HackerNews.IRepositories;
using HackerNews.Model;
using HackerNews.Repositories;
using HackerNews.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Services
{
	public class ProgramHandler
	{
		IPostRepository repo;
		public ProgramHandler()
		{
			repo = new PostRepository();
		}

		/// <summary>
		/// Runs the program.
		/// </summary>
		/// <param name="args">The arguments passed in the command line.</param>
		/// <returns>The output to be printed.</returns>
		public string Run(string[] args)
		{
			if (!IsValidInput(args, out string message))
				return message;
			
			Int32.TryParse(args[1], out int numberOfPosts);
			return GetResult(numberOfPosts);
		}

		/// <summary>
		/// Retrieves the top posts in JSON format. If it fails to retrieve the exact number of requested posts it tries again using the next ids in line.
		/// </summary>
		/// <param name="numberOfPosts">The number of posts contained in the JSON.</param>
		/// <returns>JSON containing the posts.</returns>
		public string GetResult(int numberOfPosts)
		{
			List<PostModel> postModels = new List<PostModel>();
			string[] ids = repo.GetTopPostsIds();
			int requiredPosts = numberOfPosts;
			int index = 0;

			do
			{
				List<Post> posts = repo.GetPostsByIds(ids.Skip(index).Take(requiredPosts));
				postModels.AddRange(GetPostModels(posts, postModels.Count + 1));
				index += requiredPosts;
				requiredPosts = numberOfPosts - postModels.Count;
			}
			while (postModels.Count < numberOfPosts && index < ids.Length);

			return ConvertUtils.GetJson(postModels, formatted: true);
		}

		/// <summary>
		/// Transforms the story posts into the PostModels. Note only the valid models will be returned.
		/// </summary>
		/// <param name="posts">The posts to be transformed.</param>
		/// <param name="startingRank">The starting rank number for the models.</param>
		/// <returns>The valid post models derived from the posts.</returns>
		public List<PostModel> GetPostModels(IList<Post> posts, int startingRank)
		{
			List<PostModel> postModels = new List<PostModel>();
			foreach (Post post in posts)
			{
				PostModel model = new PostModel(post, startingRank);
				if (model.IsValid())
				{
					postModels.Add(model);
					startingRank++;
				}
			}
			return postModels;
		}

		/// <summary>
		/// Evaluates the arguments.
		/// </summary>
		/// <param name="args">The arguments passed in the command line.</param>
		/// <param name="output">String to be populated in case of invalid imput.</param>
		/// <returns>True with an empty output and false with the error message to be displayed.</returns>
		private bool IsValidInput(string[] args, out string output)
		{
			bool isValid = false;
			output = "";

			if (args == null || args.Length == 0)
			{
				output = HelpMessage;
			}
			else
			{
				switch (args.Length)
				{
					case 1:
						if (args[0] == "-h")
						{
							output = HelpMessage;
						}
						else
						{
							output = CommandErrorMessage;
						}
						break;
					case 2:
						if (args[0] != "--posts")
						{
							output = CommandErrorMessage;
						}
						else if (Int32.TryParse(args[1], out int n))
						{
							if (n <= 0 || n > 100)
								output = ArgumentErrorMessage;
							else
								isValid = true;
						}
						else
						{
							output = ArgumentErrorMessage;
						}
						break;
					default:
						output = CommandErrorMessage;
						break;
				}
			}
			return isValid;
		}

		const string HelpMessage =
			"Usage: hackernews [options] [arguments]\n\n" +
			"Options:\n" +
			"-h                   Display help." +
			"--posts <NPOSTS>  Prints top posts in JSON.\n" +
			"Arguments:\n" +
			"<NPOSTS>          The number of posts to be printed. A positive integer <= 100.";

		const string CommandErrorMessage =
			"Error: Command not recognized. \n" +
			"Type '-h' to get a list of supported commands.";

		const string ArgumentErrorMessage =
			"Error: Invalid Argument. \n" +
			"Type '-h' to get a list of supported commands.";
	}
}
