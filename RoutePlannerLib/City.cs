using System;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class City
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public WayPoint Location { get; set; }

        public City (string n, string c, int p, double lat, double lon)
        {
            this.Name = n;
            this.Country = c;
            this.Population = p;
            this.Location = new WayPoint(n, lat, lon);
        }
    }
}

