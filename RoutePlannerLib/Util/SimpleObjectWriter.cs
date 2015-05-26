using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Xml.Serialization;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectWriter
    {
        public StringWriter stream { get; private set; }

        public SimpleObjectWriter(StringWriter stream)
        {
            this.stream = stream;
        }
        
        public void Next(Object c)
        {
            /*
             <--
            Instance of Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.City
            Name="Aarau"
            Country="Switzerland"
            Population=10
            Location is a nested object...
            Instance of Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.WayPoint 
            Name="Aarau"
            Longitude=2.2
            Latitude=1.1
            End of instance
            End of instance
            -->
             */

            if (stream != null && c != null)
            {
                stream.Write("Instance of {0}\r\n", c.GetType().FullName);
                foreach (var p in c.GetType().GetProperties())
                {
                    if (p.CustomAttributes.Select(x => x.AttributeType == typeof(XmlIgnoreAttribute)).Count() != 1) { 
                        var propertyValue = p.GetValue(c);
                        if (propertyValue is string)
                        {
                            stream.Write("{0}=\"{1}\"\r\n", p.Name, propertyValue);
                        }
                        else if (propertyValue is System.ValueType)
                        {
                            stream.Write("{0}={1}\r\n", p.Name, Convert.ToString(propertyValue, CultureInfo.InvariantCulture.NumberFormat));
                        }
                        else
                        {
                            stream.Write("{0} is a nested object...\r\n", p.Name);
                            this.Next(propertyValue);
                    }
                    }
                }
                stream.Write("End of instance\r\n");
            }
        }
    }
}
