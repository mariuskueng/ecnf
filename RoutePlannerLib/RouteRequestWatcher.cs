using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestWatcher
    {
        public Dictionary<string, int> requestCounter;
        
        public RouteRequestWatcher () {
            requestCounter = new Dictionary<string, int>();
        }

        public void LogRouteRequests(object sender, RouteRequestEventArgs args)
        {
            if (requestCounter.ContainsKey(args.ToCity))
            {
                requestCounter[args.ToCity] += 1;
            }
            else
            {
                requestCounter[args.ToCity] = 1;
            }

            Console.WriteLine("Current Request state");
            Console.WriteLine("---------------------");
            foreach (var city in requestCounter)
            {
                Console.WriteLine("ToCity: {0} has been requested {1} times", city.Key, city.Value);
            }
        }

        public int GetCityRequests(string cityName)
        {
            try
            {
                return requestCounter[cityName];
            }
            catch
            {
                return 0;
            }
        }
    }
}
