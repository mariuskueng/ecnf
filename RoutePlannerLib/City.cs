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

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            City c = obj as City;
            if ((System.Object)c == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.Name == c.Name) && (this.Country == c.Country);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

