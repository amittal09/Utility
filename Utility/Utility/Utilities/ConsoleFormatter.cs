using System;

namespace Vertica.Utilities_v4
{
	public class ConsoleFormatter : IDisposable
	{
		internal ConsoleFormatter(ConsoleColor foreground)
		{
			Console.ForegroundColor = foreground;
		}

		internal ConsoleFormatter(ConsoleColor foreground, ConsoleColor background) : this(foreground)
		{
			Console.BackgroundColor = background;
		}

		public void Dispose()
		{
			Console.ResetColor();
		}

		public static ConsoleFormatter Set(ConsoleColor foreground)
		{
			return new ConsoleFormatter(foreground);
		}

		public static ConsoleFormatter Set(ConsoleColor foreground, ConsoleColor background)
		{
			return new ConsoleFormatter(foreground, background);
		}
	}
}