using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Buddy.Test.PerformanceTestData
{
    public class BacklogEntry : Common
    {
        public int Backlog { get; set; }
        public DateTime LastReceivedOn { get; set; }
        public int TotalReceived { get; set; }

        public static List<BacklogEntry> Load()
        {
            var result = new List<BacklogEntry>();

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Buddy.Test.Properties.Backlog.csv";

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

        public static BacklogEntry ParseLine(string line)
        {
            var trim = new [] { ' ', '"' };

            var fields = line.Split(new [] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if(fields.Length != 6)
                throw new ArgumentException($"{nameof(ParseLine)}: Wrong number of fields found in line", nameof(line));
            
            var result = new BacklogEntry
            {
                CustomerId = int.Parse(fields[0].Trim(trim)),
                CustomerName = fields[1].Trim(trim),
                StatTime = new DateTime(DateTime.Parse(fields[2].Trim(trim)).Ticks, DateTimeKind.Utc),
                Backlog  = int.Parse(fields[3].Trim(trim)),
                LastReceivedOn = new DateTime(DateTime.Parse(fields[4].Trim(trim)).Ticks, DateTimeKind.Utc),
                TotalReceived  = int.Parse(fields[5].Trim(trim))
            };

            return result;
        }
    }
}