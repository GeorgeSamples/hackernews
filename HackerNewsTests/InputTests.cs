using HackerNews.Services;
using Xunit;

namespace HackerNewsTests
{
	public class InputTests
	{
		ProgramHandler program = new ProgramHandler();

		[Fact]
		public void NoParametersOnExecutionShouldReturnHelpMessage()
		{
			Assert.Equal(HelpMessage, program.Run(null));
		}

		[Fact]
		public void EmptyParametersOnExecutionReturnsHelpMessage()
		{
			string[] args = new string[0];
			string expected = HelpMessage;
			string actual = program.Run(args);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void HelpOptionOnExecutionReturnsHelpMessage()
		{
			string[] args = new string[1];
			args[0] = "-h";
			string expected = HelpMessage;
			string actual = program.Run(args);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void InvalidOptionOnExecutionReturnsCommandError()
		{
			string[] args = new string[1];
			args[0] = "asdf";
			string expected = CommandErrorMessage;
			string actual = program.Run(args);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void InvalidOptionWithValidArgumentOnExecutionReturnsCommandError()
		{
			string[] args = new string[2];
			args[0] = "asdf";
			args[1] = "5";
			string expected = CommandErrorMessage;
			string actual = program.Run(args);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void InvalidOptionWithInvalidArgumentOnExecutionReturnsCommandError()
		{
			string[] args = new string[2];
			args[0] = "asdf";
			args[1] = "asd";
			string expected = CommandErrorMessage;
			string actual = program.Run(args);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ValidOptionWithInvalidZeroArgumentOnExecutionReturnsArgumentError()
		{
			string[] args = new string[2];
			args[0] = "--posts";
			args[1] = "0";
			string expected = ArgumentErrorMessage;
			string actual = program.Run(args);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ValidOptionWithInvalidStringArgumentOnExecutionReturnsArgumentError()
		{
			string[] args = new string[2];
			args[0] = "--posts";
			args[1] = "asd";
			string expected = ArgumentErrorMessage;
			string actual = program.Run(args);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ValidOptionWithInvalidCharArgumentOnExecutionReturnsArgumentError()
		{
			string[] args = new string[2];
			args[0] = "--posts";
			args[1] = "a";
			string expected = ArgumentErrorMessage;
			string actual = program.Run(args);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ValidOptionWithInvalidNegativeArgumentOnExecutionReturnsArgumentError()
		{
			string[] args = new string[2];
			args[0] = "--posts";
			args[1] = "-5";
			string expected = ArgumentErrorMessage;
			string actual = program.Run(args);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ValidOptionWithInvalidTooBigArgumentOnExecutionReturnsArgumentError()
		{
			string[] args = new string[2];
			args[0] = "--posts";
			args[1] = "101";
			string expected = ArgumentErrorMessage;
			string actual = program.Run(args);
			Assert.Equal(expected, actual);
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
