using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestEventArgs : System.EventArgs
    {
        public string fromCity;
        public string toCity;
        public TransportModes transportMode;

        public RouteRequestEventArgs(string from, string to, TransportModes mode)
        {
            this.fromCity = from;
            this.toCity = to;
            this.transportMode = mode;
        }
    }
}
