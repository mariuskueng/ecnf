using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Properties;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RoutesFactory
    {
        static public IRoutes Create(Cities cities)
        {
            return Create(cities, Settings.Default.RouteAlgorithm);
        }

        static public IRoutes Create(Cities cities, string algorithmClassName)
        {
            try
            {
                Assembly routeLib = Assembly.LoadFrom("RoutePlannerLib.dll");
                Type routeClass = routeLib.GetType(algorithmClassName);
                if (routeClass != null)
                {
                    ConstructorInfo routesConstructer = routeClass.GetConstructor(new [] { typeof(Cities) });
                    object routerInstance = routesConstructer.Invoke(new object[] {cities});
                    return (IRoutes) routerInstance;
                }
            }
            catch (DllNotFoundException)
            {

                throw new DllNotFoundException("RoutePlannerLib.dll not found.");
            }

            return null;
            
        }
    }
}
