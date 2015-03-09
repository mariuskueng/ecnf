using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        public List<City> citiesList;
        public int Count {
            get { return citiesList.Count; }
        }

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
            TextReader reader = new StreamReader(filename);
            string line;
            var count = 0;
            while ((line = reader.ReadLine()) != null)
            {
                var lineContents = line.Split('\t');
                citiesList.Add(new City(
                    lineContents[0],
                    lineContents[1],
                    Convert.ToInt32(lineContents[2]),
                    Convert.ToDouble(lineContents[3], CultureInfo.InvariantCulture.NumberFormat),
                    Convert.ToDouble(lineContents[4], CultureInfo.InvariantCulture.NumberFormat)
                ));
                count++;
            }
            reader.Close();
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
    }
}

