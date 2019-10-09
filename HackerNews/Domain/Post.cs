namespace HackerNews.Domain
{
	public class Post
	{
		public Post() { }

		public string Title { get; set; }
		public string Url { get; set; }
		public string By { get; set; }
		public long Time { get; set; }
		public int Score { get; set; }
		public int Descendants { get; set; }
		public string Type { get; set; }

		public bool IsStory()
		{
			return Type == "story";
		}
	}
}
