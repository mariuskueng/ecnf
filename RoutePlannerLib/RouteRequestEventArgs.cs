using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestEventArgs : System.EventArgs
    {
        public string FromCity {get; private set;}
        public string ToCity { get; private set; }
        public TransportModes transportMode;

        public RouteRequestEventArgs(string from, string to, TransportModes mode)
        {
            this.FromCity = from;
            this.ToCity = to;
            this.transportMode = mode;
        }
    }
}
