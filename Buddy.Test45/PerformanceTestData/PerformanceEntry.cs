using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Buddy.Test.PerformanceTestData
{
    public class PerformanceEntry : Common
    {
        public int DatabaseConnections { get; set; }
        public int IdleDatabaseConnections { get; set; }
        public int TotalDatabaseConnections { get; set; }
        public int TotalIdleDatabaseConnections { get; set; }
        public double PctProcessorTime { get; set; }
        public int AvailableMBytes { get; set; }
        public double PctPagingFileUsage { get; set; }

        public static List<PerformanceEntry> Load()
        {
            var result = new List<PerformanceEntry>();

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Buddy.Test.Properties.Performance.csv";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    // Read the column line
                    if(reader.EndOfStream)
                        return result;
                    reader.ReadLine();

                    while(!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        result.Add(ParseLine(line));
                    }
                }
            }

            return result;
        }

        public static PerformanceEntry ParseLine(string line)
        {
            var trim = new [] { ' ', '"' };

            var fields = line.Split(new [] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if(fields.Length != 10)
                throw new ArgumentException($"{nameof(ParseLine)}: Wrong number of fields found in line", nameof(line));
            
            var result = new PerformanceEntry
            {
                CustomerId = int.Parse(fields[0].Trim(trim)),
                CustomerName = fields[1].Trim(trim),
                StatTime = new DateTime(DateTime.Parse(fields[2].Trim(trim)).Ticks, DateTimeKind.Utc),
                DatabaseConnections  = int.Parse(fields[3].Trim(trim)),
                IdleDatabaseConnections = int.Parse(fields[4].Trim(trim)),
                TotalDatabaseConnections  = int.Parse(fields[5].Trim(trim)),
                TotalIdleDatabaseConnections = int.Parse(fields[6].Trim(trim)),
                PctProcessorTime  = double.Parse(fields[7].Trim(trim)),
                AvailableMBytes  = int.Parse(fields[8].Trim(trim)),
                PctPagingFileUsage = double.Parse(fields[9].Trim(trim))
            };

            return result;
        }
    }
}