using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Garbage
{
    class Program
    {
        /// <summary>
        /// Settings for Garbage Collector in App.config (change manually!)
        /// 
        /// manually tested: average Duration of 10 runs without highest 2 and lowest 2
        /// 
        /// gcConcurrent = true,  gcServer = true   >  Duration: 1427 ms
        /// gcConcurrent = true,  gcServer = false  >  Duration: 1363 ms
        /// gcConcurrent = false, gcServer = true   >  Duration: 421 ms
        /// gcConcurrent = false, gcServer = false  >  Duration: 407 ms
        /// 
        /// Garbage Collection seems to work best if not run concurrently (which is not default)
        /// It doesn't make a big differenc if GC is run on the server 
        /// but not running it on the server seems better (which is default)
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Total Memory before: {0}", GC.GetTotalMemory(false));
            stopwatch.Start();

            // viel Garbage wird erzeugt (unreferenzierte Objekte à ca. 100MB)
            GenerateGarbage(1000);

            // Garbage Collector wird automatisch aufgerufen.
            // Von manuellem Aufruf wird grundsätzlich abgeraten.

            stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0} ms", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Total Memory after: {0}", GC.GetTotalMemory(false));
            Console.ReadKey();
        }

        public static void GenerateGarbage(int numOfObjects) {
            for (int i = 0; i < numOfObjects; i++)
            {
                var temp = new byte[1024 * 1024 * 100]; // ca. 100 MB
                temp = null;
            }
        }
    }
}
