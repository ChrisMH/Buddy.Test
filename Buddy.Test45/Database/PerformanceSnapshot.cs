using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Buddy.Test.Database
{
    public class PerformanceSnapshot
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public DateTime StatTime { get; set; }

        public int Backlog { get; set; }

        public DateTime LastReceivedOn { get; set; }

        public int TotalReceived { get; set; }

        public int DatabaseConnections { get; set; }

        public int IdleDatabaseConnections { get; set; }

        public double PctProcessorTime { get; set; }

        public int AvailableMBytes { get; set; }

        public double PctPagingFileUsage { get; set; }
        
        public static List<PerformanceSnapshot> Load()
        {
            var result = new List<PerformanceSnapshot>();

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Buddy.Test.Properties.PerformanceSnapshots.csv";

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

        public static PerformanceSnapshot ParseLine(string line)
        {
            var trim = new [] { ' ', '"' };

            var fields = line.Split(new [] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if(fields.Length != 11)
                throw new ArgumentException($"{nameof(ParseLine)}: Wrong number of fields found in line", nameof(line));
            
            var result = new PerformanceSnapshot
            {
                CustomerId = int.Parse(fields[0].Trim(trim)),
                CustomerName = fields[1].Trim(trim),
                StatTime = new DateTime(DateTime.Parse(fields[2].Trim(trim)).Ticks, DateTimeKind.Utc),
                Backlog = int.Parse(fields[3].Trim(trim)),
                LastReceivedOn = new DateTime(DateTime.Parse(fields[4].Trim(trim)).Ticks, DateTimeKind.Utc),
                TotalReceived = int.Parse(fields[5].Trim(trim)),
                DatabaseConnections  = int.Parse(fields[6].Trim(trim)),
                IdleDatabaseConnections = int.Parse(fields[7].Trim(trim)),
                PctProcessorTime  = double.Parse(fields[8].Trim(trim)),
                AvailableMBytes  = int.Parse(fields[9].Trim(trim)),
                PctPagingFileUsage = double.Parse(fields[10].Trim(trim))
            };

            return result;
        }
    }
}