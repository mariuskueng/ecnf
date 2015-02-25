﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        public List<City> cities;
        public int count {
            get { return cities.Count; }
        }

        public Cities ()
        {
            this.cities = new List<City> ();
        }

        public City this[int index] //indexer implementation 
        {
            get { 
                try {
                    return this.cities[index];
                } catch (ArgumentOutOfRangeException ex) {
                    return null;
                }
            }
            set { this.cities[index] = value; }
        }

        public int ReadCities(string filename)
        {
            TextReader reader = new StreamReader(filename);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var lineContents = line.Split ('\t'); 
                cities.Add(new City(
                    lineContents[0], 
                    lineContents[1], 
                    int.Parse(lineContents[2]), 
                    double.Parse(lineContents[3]), 
                    double.Parse(lineContents[4])
                ));
            }
            reader.Close();
            return cities.Count;
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            List<City> neighbours = new List<City>();
            for (int i = 0; i < count; i++) {
                if (cities [i].Location.Distance (location) < distance) {
                    neighbours.Add(cities[i]);
                }
            }
//            neighbours = neighbours.Sort((x, y) => x.Location.Distance.compareTo(y.Location.Distance));
            return neighbours;
        }
    }
}

