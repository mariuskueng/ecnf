using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class WayPoint
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public WayPoint(string _name, double _latitude, double _longitude)
        {
            Name = _name;
            Latitude = _latitude;
            Longitude = _longitude;
        }

        public override string ToString()
        {
            string output = "Waypoint: ";
            
            if (string.IsNullOrEmpty(this.Name))
            {
                output += string.Format("{0} ", this.Name);
            }
            output += string.Format("{0}/{1}", Math.Round(this.Latitude, 2), Math.Round(this.Longitude, 2));
            return output;
        }
    }
}
