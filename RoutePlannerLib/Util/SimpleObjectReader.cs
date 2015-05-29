using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Globalization;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectReader
    {
        public StringReader stream { get; private set; }

        public SimpleObjectReader(StringReader stream)
        {
            this.stream = stream;
        }

        public Object Next()
        {
            if (stream != null)
            {
                var lines = stream.ReadLine();

                Type type = Type.GetType(lines.Split(' ')[2]);
                Assembly objAssembly = type.Assembly;
                Object obj = objAssembly.CreateInstance(type.FullName);

                while (lines != null && obj != null && !lines.Contains("End of instance"))
                    // match end of instance otherwise it won't create a new root instance
                {
                    String[] line;
                    PropertyInfo property;

                    if (lines.Contains("is a nested object"))
                    {
                        line = lines.Split(' ');
                        property = obj.GetType().GetProperty(line[0]);
                        property.SetValue(obj, this.Next());
                    }
                    else
                    {
                        line = lines.Split('=');
                        property = obj.GetType().GetProperty(line[0]);
                 
                        // if 'Instance of' occurs there won't be a '='
                        if (property != null)
                        {
                            Object objValue = property.GetValue(obj);

                            if (objValue is string)
                            {
                                property.SetValue(obj, line[1].Trim('\"'));
                            }
                            else if (objValue is System.ValueType)
                            {
                                if (objValue is Int32)
                                {
                                    property.SetValue(obj, Int32.Parse(line[1]));
                                }
                                else if (objValue is double)
                                {
                                    property.SetValue(obj, double.Parse(line[1], CultureInfo.InvariantCulture.NumberFormat));
                                }
                            }
                        }
                    }
                    lines = stream.ReadLine();
                }
                
                return obj;
            }
            return null;
        }
    }
}
