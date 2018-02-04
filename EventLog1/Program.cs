using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLog1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {

                string eventID1 = "6005";
                string eventID2 = "6006";
                string LogSource = "System";
                string sQuery = $"*[System/EventID=\"{eventID1}\" or System/EventID=\"{eventID2}\"]";

                Console.WriteLine($"Query {sQuery}");

                var elQuery = new EventLogQuery(LogSource, PathType.LogName, sQuery);
                var elReader = new System.Diagnostics.Eventing.Reader.EventLogReader(elQuery);
                List<EventRecord> eventList = new List<EventRecord>();
                for (EventRecord eventInstance = elReader.ReadEvent();
                    null != eventInstance; eventInstance = elReader.ReadEvent())
                {
                    eventList.Add(eventInstance);
                    string eventName = "No event";
                    if (eventInstance.Id == 6005)
                    {
                        eventName = "Computer startup";
                    }
                    else if (eventInstance.Id == 6006)
                    {
                        eventName = "Computer shutdown";
                    }
                    Console.WriteLine($"{eventName}: {eventInstance.TimeCreated}");
                }

            }
            catch (Exception ex)
            {
                var codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                var progname = Path.GetFileNameWithoutExtension(codeBase);
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }

        }
    }
}
