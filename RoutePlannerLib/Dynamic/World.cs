using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Dynamic
{
    public class World : DynamicObject
    {
        private Cities cities;
        public World(Cities c)
        {
            cities = c;
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, Object[] args, out Object result)
        {
            City city = cities.FindCity(binder.Name);
            if (city != null)
            {
                result = city;
            }
            else
            {
                result = String.Format("The city \"{0}\" does not exist!", binder.Name);
            }

            return true;
        }
    }
}
