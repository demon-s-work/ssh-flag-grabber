using System.Drawing;
using Console=Colorful.Console;

namespace ssh_flag_grabber
{
	public static class Logger
	{
		public static void Ascii(string text, Color color)
		{
			Console.WriteAscii(text, color);
		}

		public static void Verb(string text)
		{
			if (!Program.verbose)
				return;
			ConsoleLog(text, Color.LimeGreen);
		}

		public static void Err(string text)
		{
			ConsoleLog(text, Color.Red);
		}

		public static void Log(string text)
		{
			ConsoleLog(text, Color.Yellow);
		}

		private static void ConsoleLog(string text, Color color)
		{
			Console.WriteLine($"[{DateTime.Now.TimeOfDay.ToString()}]\t{text}", color);
		}

		public static void WriteAtBottom(string leftText, string rightText)
		{
			Console.SetCursorPosition(0, Console.CursorTop);
			for (int i = 0; i < Console.BufferWidth; i++)
			{
				Console.Write(' ');
			}
			Console.SetCursorPosition(0, Console.CursorTop);
			Console.Write(leftText, Color.Aqua);

			Console.SetCursorPosition(Console.BufferWidth - rightText.Length, Console.CursorTop);
			Console.Write(rightText, Color.Aqua);
		}
	}
}