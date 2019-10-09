using HackerNews.Services;
using System;

namespace HackerNews
{
	class Program
	{
		static void Main(string[] args)
		{
			ProgramHandler program = new ProgramHandler();
			Console.WriteLine(program.Run(args));
		}
	}
}
