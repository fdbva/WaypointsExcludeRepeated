using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WaypointsExcludeRepeated
{
    class Program
    {
        private const string InputName = "input.wpt";
        private const string OutputName = "output.wpt";
        private const int HeaderSize = 4;

        static void Main(string[] args)
        {
            var directory = Directory.GetCurrentDirectory();
            Console.WriteLine(directory);

            var allLines = ReadFile(directory);
            if (allLines.Length <= HeaderSize)
            {
                Console.WriteLine("Waypoints not found in file | Waypoints não encontrados no arquivo");
                Console.ReadKey();
                return;
            }

            var header = allLines.Take(HeaderSize);
            var waypointLines = allLines.Skip(HeaderSize);

            var outputWaypoints = RemoveDuplicates(waypointLines);

            var outputLines = header.Concat(outputWaypoints);
            WriteFile(directory, outputLines);

            Console.WriteLine($"Input waypoints: {waypointLines.Count()}");
            Console.WriteLine($"Output waypoints: {outputWaypoints.Count()}");
            Console.ReadKey();
        }

        private static void WriteFile(string directory, IEnumerable<string> outputLines)
        {
            var outputPath = Path.Combine(directory, OutputName);
            File.WriteAllLines(outputPath, outputLines);
        }

        private static IEnumerable<string> RemoveDuplicates(IEnumerable<string> waypointLines)
        {
            var waypointsDictionary = new Dictionary<string, string>();
            foreach (var line in waypointLines)
            {
                var values = line.Split(',');
                waypointsDictionary.TryAdd($"{values[2]}{values[2]}{values[2]}", line);
            }

            var outputWaypoints = waypointsDictionary.Select(x => x.Value);
            return outputWaypoints;
        }

        private static string[] ReadFile(string directory)
        {
            var inputPath = Path.Combine(directory, InputName);

            return File.ReadAllLines(inputPath);
        }
    }
}
