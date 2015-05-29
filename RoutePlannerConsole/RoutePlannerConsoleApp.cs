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
            var rio = new WayPoint ("Rio de Janeiro", -22.908970, -43.175922);

            Console.WriteLine(Math.Round(bern.Distance(tripolis), 2));

            Cities staedte = new Cities ();
            staedte.ReadCities ("citiesTestDataLab2.txt");

            Cities staedte2 = new Cities();
            staedte2.ReadCities("nofile.txt");
           
            Console.WriteLine (staedte.Count);
            try
            {
                Console.WriteLine(staedte[0].Name);
            }
            catch (Exception)
            {
                throw;
            }
            

            try {
                Console.WriteLine (staedte [10].Name);
            } catch (NullReferenceException) {
                Console.WriteLine ("City doesn't exist");
            }

            foreach (var city in staedte.FindNeighbours (rio, 1000)) {
                Console.WriteLine (city.Name);    
            }

            Routes routes = new RoutesDijkstra(staedte);
            Console.WriteLine("Routes count={0}", routes.ReadRoutes("linksTestDataLab3.txt"));

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();

        }
    }
}
