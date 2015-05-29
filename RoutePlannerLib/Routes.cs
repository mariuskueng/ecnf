
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Diagnostics;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    /// <summary>
    /// Manages a routes from a city to another city.
    /// </summary>
    
    public delegate void RouteRequestHandler(object sender, RouteRequestEventArgs e);

    public abstract class Routes : IRoutes
    {
        protected List<Link> routes = new List<Link>();
        protected Cities cities;
        public event RouteRequestHandler RouteRequestEvent;
        private static TraceSource routesLogger =
           new TraceSource("Routes");
        public bool ExecuteParallel { set; get; }

        public int Count
        {
            get { return routes.Count; }
        }

        /// <summary>
        /// Initializes the Routes with the cities.
        /// </summary>
        /// <param name="cities"></param>
        public Routes(Cities cities)
        {
            this.cities = cities;
        }

        public void NotifyObservers(Object sender, RouteRequestEventArgs args)
        {
            if (RouteRequestEvent != null)
                RouteRequestEvent(sender, args);
        }

        /// <summary>
        /// Reads a list of links from the given file.
        /// Reads only links where the cities exist.
        /// </summary>
        /// <param name="filename">name of links file</param>
        /// <returns>number of read route</returns>
        public int ReadRoutes(string filename)
        {
            routesLogger.TraceEvent(TraceEventType.Information, 1, "ReadRoutes started");
            using (TextReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var linkAsString = line.Split('\t');

                    City city1 = cities.FindCity(linkAsString[0]);
                    City city2 = cities.FindCity(linkAsString[1]);

                    // only add links, where the cities are found 
                    if ((city1 != null) && (city2 != null))
                    {
                        routes.Add(new Link(city1, city2, city1.Location.Distance(city2.Location),
                                                   TransportModes.Rail));
                    }
                }
            }
            routesLogger.TraceEvent(TraceEventType.Information, 2, "ReadRoutes ended");
            return Count;

        }

        public abstract List<Link> FindShortestRouteBetween(string fromCity, string toCity,
                                        TransportModes mode, IProgress<string> reportProgress);

        public List<Link> FindShortestRouteBetween(string fromCity, string toCity,
                                        TransportModes mode)
        {
            return FindShortestRouteBetween(fromCity, toCity, mode, null);
        }
     
        public Task<List<Link>> FindShortestRouteBetweenAsync(string fromCity, string toCity,
                                        TransportModes mode)
        {
            return Task.Run(() => FindShortestRouteBetween(fromCity, toCity, mode));
        }

        public Task<List<Link>> FindShortestRouteBetweenAsync(string fromCity, string toCity,
                                        TransportModes mode, IProgress<string> reportProgress)
        {
            return Task.Run(() => FindShortestRouteBetween(fromCity, toCity, mode, reportProgress));
        }

        protected static List<City> FillListOfNodes(List<City> cities, out Dictionary<City, double> dist, out Dictionary<City, City> previous)
        {
            var q = new List<City>(); // the set of all nodes (cities) in Graph ;
            dist = new Dictionary<City, double>();
            previous = new Dictionary<City, City>();
            foreach (var v in cities)
            {
                dist[v] = double.MaxValue;
                previous[v] = null;
                q.Add(v);
            }

            return q;
        }

        protected List<Link> FindPath(List<City> citiesOnRoute, TransportModes mode)
        {
            var cityLinks = new List<Link>();
            for (int i = 0; i < citiesOnRoute.Count - 1; i++)
            {
                cityLinks.Add(FindLink(citiesOnRoute[i], citiesOnRoute[i + 1], mode));
            }
            return cityLinks;
        }

        protected Link FindLink(City u, City n, TransportModes mode)
        {
            return new Link(u, n, u.Location.Distance(n.Location), mode);
        }

        /// <summary>
        /// Lab 6 Aufgabe 3, Städte die mind. in einer Route vorkommen
        /// </summary>
        /// <param name="transportMode">transportation mode</param>
        /// <returns>list of cities</returns>
        public City[] FindCities(TransportModes transportMode)
        {
            var fromCities = routes.Where(r => r.TransportMode.Equals(transportMode))
                .Select(f => f.FromCity).Distinct();
            var toCities = routes.Where(r => r.TransportMode.Equals(transportMode))
                .Select(t => t.ToCity).Distinct();
            return fromCities.Union(toCities).ToArray();
        }

        protected List<City> GetCitiesOnRoute(City source, City target, Dictionary<City, City> previous)
        {
            var citiesOnRoute = new List<City>();
            var cr = target;
            while (previous[cr] != null)
            {
                citiesOnRoute.Add(cr);
                cr = previous[cr];
            }
            citiesOnRoute.Add(source);

            citiesOnRoute.Reverse();
            return citiesOnRoute;
        }
    }
}
