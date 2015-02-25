using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RoutePlannerConsoleApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to RoutePlanner ({0})",
                Assembly.GetExecutingAssembly().GetName().Version);

            var wayPoint = new WayPoint("Windisch", 47.479319847061966, 8.212966918945312);
            Console.WriteLine(wayPoint.ToString());
            var wayPoint2 = new WayPoint(null, 47.479319847061966, 8.212966918945312);
            Console.WriteLine(wayPoint2.ToString());

            var bern = new WayPoint ("Bern", 46.9479220, 7.4446080);
            var tripolis = new WayPoint ("Tripolis", 32.8133110, 13.1048450);

            Console.WriteLine(bern.Distance(tripolis));

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}
