using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;

// NOTE: This sample program scaffolds a TPS→CSV dumper.
// It expects a Topspeed reader NuGet package to be added (see csproj).
// Replace ITopSpeedReader usage with the actual API from the chosen package.

namespace TpsToCsv
{
    class Program
    {
        static int Main(string[] args)
        {
            var input = new Option<string>(name: "--input", description: "Input directory containing .TPS/.DAT") { IsRequired = true };
            var output = new Option<string>(name: "--output", description: "Output directory for CSV files") { IsRequired = true };

            var root = new RootCommand("Dump Clarion TopSpeed tables to CSV");
            root.AddOption(input);
            root.AddOption(output);

            root.SetHandler((string inp, string outp) => Run(inp, outp), input, output);
            return root.Invoke(args);
        }

        static void Run(string inputDir, string outputDir)
        {
            Directory.CreateDirectory(outputDir);
            var patterns = new[] { "*.TPS", "*.tps", "*.DAT", "*.dat" };
            var files = patterns.SelectMany(p => Directory.GetFiles(inputDir, p)).Distinct().ToArray();
            Console.WriteLine($"Found {files.Length} files in {inputDir}");

            foreach (var path in files)
            {
                var name = Path.GetFileNameWithoutExtension(path);
                var csv = Path.Combine(outputDir, name + ".csv");
                Console.WriteLine($"Exporting {Path.GetFileName(path)} → {csv}");

                // TODO: Use actual Topspeed reader API
                // using var reader = new TopSpeedReader(path);
                // var table = reader.ReadAll();
                // CsvWriter.Write(table, csv);

                // Placeholder to avoid compile errors before adding the package
                File.WriteAllText(csv, "TODO: add Topspeed reader NuGet and implement dump");
            }
        }
    }
}
