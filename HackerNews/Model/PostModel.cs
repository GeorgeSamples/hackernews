using HackerNews.Domain;
using System;

namespace HackerNews.Model
{
	public class PostModel
	{
		private const int MaxStringSize = 256;

		public PostModel() { }

		public PostModel(Post post, int rank)
		{
			Load(post , rank);
		}

		private string title { set; get; }
		public string Title
		{
			get
			{
				return title;
			}
			set
			{
				title = ManipulateString(value);
			}
		}
		public string Uri { get; set; }
		private string author { set; get; }
		public string Author
		{
			get
			{
				return author;
			}
			set
			{
				author = ManipulateString(value);
			}
		}
		public int Points { get; set; }
		public int Comments { get; set; }
		public int Rank { get; set; }

		public bool IsValid()
		{
			return !(Points < 0 || Rank < 0 || Comments < 0 || string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Author) || !System.Uri.IsWellFormedUriString(Uri, UriKind.Absolute));
		}

		private string ManipulateString(string input)
		{
			if (!string.IsNullOrWhiteSpace(input) && input.Length > MaxStringSize)
			{
				input = input.Substring(0, MaxStringSize - 3) + "...";
			}
			return input;
		}

		private void Load(Post post, int rank)
		{
			if (post != null && post.IsStory())
			{
				Title = post.Title;
				Uri = post.Url;
				Author = post.By;
				Points = post.Score;
				Comments = post.Descendants;
				Rank = rank;
			}
		}
	}
}
