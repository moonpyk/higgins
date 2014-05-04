using System;
using System.Collections.Generic;
using System.Linq;
using Higgins.Core.Lib;

namespace Higgins.Core
{
    public class GitLogResult
    {
        public GitLogResult(string log)
        {
            Entries = new List<LogEntry>();
            foreach (var l in log.Trim().Split('\n').Select(s => s.Trim().Split('|')).Where(l => l.Length >= 5))
            {
                int dtInt;
                if (!int.TryParse(l[1], out dtInt))
                {
                    continue;
                }

                var e = new LogEntry
                {
                    Hash = l[0],
                    Date = dtInt.FromUnixTime(),
                    Author = l[2],
                    Email = l[3],
                    Message = l[4]
                };

                Entries.Add(e);
            }
        }

        public List<LogEntry> Entries
        {
            get;
            private set;
        }

        public class LogEntry
        {
            public string Hash { get; set; }

            public DateTime Date { get; set; }

            public string Author { get; set; }

            public string Email { get; set; }

            public string Message { get; set; }
        }
    }
}