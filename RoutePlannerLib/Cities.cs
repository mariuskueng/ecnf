using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        public List<City> citiesList;
        public int count {
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
            }
            reader.Close();
            return citiesList.Count;
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            List<City> neighbours = new List<City>();
            for (int i = 0; i < count; i++) {
                if (citiesList [i].Location.Distance (location) < distance) {
                    neighbours.Add(citiesList[i]);
                }
            }
//            neighbours = neighbours.Sort((x, y) => x.Location.Distance.compareTo(y.Location.Distance));
            return neighbours;
        }
    }
}

