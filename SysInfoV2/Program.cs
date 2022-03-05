using BetterConsoles.Colors.Extensions;
using BetterConsoles.Core;
using BetterConsoles.Tables;
using BetterConsoles.Tables.Builders;
using BetterConsoles.Tables.Configuration;
using BetterConsoles.Tables.Models;

using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace SysInfoNetV2
{
	internal static class Program
	{
		static void Main()
		{
			/* Console Table packages
				https://github.com/khalidabuhakmeh/ConsoleTables
				https://github.com/douglasg14b/BetterConsoleTables
				https://github.com/yakivyusin/YetAnotherConsoleTables
			*/

			// Display all console color combinations (16 x 15 = 240)
			foreach (ConsoleColor backColor in Enum.GetValues(typeof(ConsoleColor)))
			{
				Console.BackgroundColor = backColor;
				//Console.WriteLine($"Console.BackgroundColor = ConsoleColor.{Enum.GetName(typeof(ConsoleColor), backColor)}");
				foreach (ConsoleColor foreColor in Enum.GetValues(typeof(ConsoleColor)))
				{
					Console.ForegroundColor = foreColor;
					if (backColor != foreColor)
						Console.WriteLine($"Console.ForegroundColor = ConsoleColor.{Enum.GetName(typeof(ConsoleColor), foreColor)}");
				}
			}
			Console.ResetColor(); // reset to default values
			Console.WriteLine();

			// "UnicodeAlt" Table
			Table table = new Table("FUNCTION", "RESULT")
			{
				Config = TableConfig.UnicodeAlt()
			};
			table.AddRow("Directory.GetCurrentDirectory()", Directory.GetCurrentDirectory())
				 .AddRow($"Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@file:\\, \"\")",
					Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@"file:\", ""));
			Console.Write(table.ToString());

			// "Unicode" Table with colors
			table = new TableBuilder(TableConfig.Unicode())
				.AddColumn(new Column("FUNCTION", new CellFormat
				{
					ForegroundColor = Color.DarkSlateBlue,
					BackgroundColor = Color.White
				}))
				.AddColumn(new Column("RESULT", new CellFormat
				{
					ForegroundColor = Color.DarkSlateBlue,
					BackgroundColor = Color.White
				}))
				.Build()
				.AddRow(new Cell[]
				{
					new Cell("Directory.GetCurrentDirectory()", new CellFormat { ForegroundColor = Color.DeepSkyBlue }),
					new Cell(Directory.GetCurrentDirectory(), new CellFormat { ForegroundColor = Color.Coral })
				})
				.AddRow(new Cell[]
				{
					new Cell($"Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@file:\\, \"\")",
						new CellFormat { ForegroundColor = Color.DeepSkyBlue }),
					new Cell(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@"file:\", ""),
						new CellFormat { ForegroundColor = Color.Coral }),
				});

			Console.Write(table.ToString());

			// "Unicode" Table with  formatters
			table = new TableBuilder(TableConfig.Unicode())
				.AddColumn("Date")
				.RowFormatter<string>(x => ForegroundColor(x, Color.HotPink))
				.AddColumn("Money", new CellFormat { Alignment = Alignment.Right })
				.RowFormatter<double>(x => FormatMoney(x)) // All values in this column will be passed into the formatter
				.RowsAlignment(Alignment.Right)
				.Build()
				.AddRow("04/15/2018", 4678.23d)
				.AddRow("05/21/2019", -1954d)
				.AddRow("07/02/2019", 321.10d);

			Console.Write(table.ToString());
			Console.ReadLine();
		}

		#region Formatters
		public static string ForegroundColor(string value, Color color)
		{
			return value.Color(color, ColorPlane.Foreground);
		}

		public static string BackgroundColor(string value, Color color)
		{
			return value.Color(color, ColorPlane.Background);
		}

		public static string FormatMoney(double value)
		{
			Color green = Color.FromArgb(152, 168, 75);
			Color red = Color.IndianRed;
			string valueStr = string.Format("{0:$#.00}", value);
			valueStr = valueStr.ForegroundColor(value >= 0 ? green : red);

			return valueStr;
		}
		#endregion
	}
}
