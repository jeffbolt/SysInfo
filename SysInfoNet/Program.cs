using BetterConsoleTables;

using System;
using System.IO;
using System.Reflection;

namespace SysInfoNet
{
	internal class Program
	{
		static void Main()
		{
			/* Console Table packages
				https://github.com/khalidabuhakmeh/ConsoleTables
				https://github.com/douglasg14b/BetterConsoleTables
				https://github.com/yakivyusin/YetAnotherConsoleTables
			*/

			Table table = new Table("FUNCTION", "RESULT")
			{
				Config = TableConfiguration.Unicode()
			}
			.AddRow("Directory.GetCurrentDirectory()", Directory.GetCurrentDirectory())
			.AddRow($"Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@file:\\, \"\")",
				Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@"file:\", ""));
			
			Console.Write(table.ToString());

			Console.ReadLine();
		}
	}
}
