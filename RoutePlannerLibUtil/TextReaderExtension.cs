using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLibUtil
{
    public static class TextReaderExtension
    {
        public static IEnumerable<string> GetSplittedLines(this string separationChar)
        {
            foreach (var line in this.ReadLine())
            {
                yield return line.split(separationChar);
            }
        }
    }
}
