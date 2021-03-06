﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Diagnostics;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        public List<City> citiesList;
        public int Count {
            get { return citiesList.Count; }
        }
        private static TraceSource citiesLogger =
            new TraceSource("Cities");

        public Cities ()
        {
            this.citiesList = new List<City> ();
        }

        public City this[int index] //indexer implementation 
        {
            get { 
                try {
                    return this.citiesList[index];
                } catch (ArgumentOutOfRangeException) {
                    return null;
                }
            }
            set { this.citiesList[index] = value; }
        }

        public int ReadCities(string filename)
        {
            citiesLogger.TraceEvent(TraceEventType.Information, 1, "ReadCities started");
            var count = 0;
            try { 
                using (TextReader reader = new StreamReader(filename))
                {
                    List<string[]> citiesAsStrings = reader.GetSplittedLines('\t').ToList();
                    count = citiesAsStrings.Count;

                    citiesList.AddRange(
                        citiesAsStrings.Select(
                            c => new City(
                                    c[0].Trim(),
                                    c[1].Trim(),
                                    Convert.ToInt32(c[2]),
                                    Convert.ToDouble(c[3], CultureInfo.InvariantCulture.NumberFormat),
                                    Convert.ToDouble(c[4], CultureInfo.InvariantCulture.NumberFormat)
                            )
                        )
                     );
                
                }
                citiesLogger.TraceEvent(TraceEventType.Information, 2, "ReadCities ended");
            }
            catch (IOException e)
            {
                citiesLogger.TraceEvent(TraceEventType.Critical, 3, e.StackTrace);
            }
            return count;
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            var neighbours = citiesList.Where(c => location.Distance(c.Location) <= distance);
            return neighbours.OrderBy(o => location.Distance(o.Location)).ToList();

        }

        public City FindCity(string cityName)
        {
            return citiesList.Find(delegate(City city)
            {
                return city.Name.Equals(cityName, StringComparison.InvariantCultureIgnoreCase);
            });
        }

        /// <summary>
        /// Find all cities between 2 cities 
        /// </summary>
        /// <param name="from">source city</param>
        /// <param name="to">target city</param>
        /// <returns>list of cities</returns>
        public List<City> FindCitiesBetween(City from, City to)
        {
            var foundCities = new List<City>();
            if (from == null || to == null)
                return foundCities;

            foundCities.Add(from);

            var minLat = Math.Min(from.Location.Latitude, to.Location.Latitude);
            var maxLat = Math.Max(from.Location.Latitude, to.Location.Latitude);
            var minLon = Math.Min(from.Location.Longitude, to.Location.Longitude);
            var maxLon = Math.Max(from.Location.Longitude, to.Location.Longitude);

            // rename the name of the "cities" variable to your name of the internal City-List
            foundCities.AddRange(citiesList.FindAll(c =>
                c.Location.Latitude > minLat && c.Location.Latitude < maxLat
                        && c.Location.Longitude > minLon && c.Location.Longitude < maxLon));

            foundCities.Add(to);
            foundCities = InitIndexForAlgorithm(foundCities);
            return foundCities;
        }

        private List<City> InitIndexForAlgorithm(List<City> foundCities)
        {
            // set index for FloydWarshall
            for (var index = 0; index < foundCities.Count; index++) foundCities[index].Index = index;
            return foundCities;
        }
    }
}

