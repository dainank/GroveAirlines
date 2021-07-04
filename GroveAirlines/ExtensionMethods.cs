using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroveAirlines
{
    internal static class ExtensionMethods
    {   // files in same assembly
        internal static bool IsPositiveInteger(this int input)
        {
            return input >= 0;
        }
    }
}

