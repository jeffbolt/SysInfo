// New C# templates generate top-level statements
// See https://aka.ms/new-console-template for more information
// Top-level statements - programs without Main methods
// https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements

using BetterConsoleTables;
using System.Reflection;

var table = new Table
{
	Config = TableConfiguration.Unicode()
}
.AddColumn("FUNCTION", Alignment.Center, Alignment.Center)
.AddColumn("RESULT", Alignment.Center, Alignment.Center)
.AddRow($"Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@file:\\, \"\")",
	Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@"file:\", ""));

Console.Write(table.ToString());
Console.ReadLine();