using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer;
using Microsoft.EntityFrameworkCore;

namespace GroveAirlines_Tests.Stubs
{
    class GroveAirlinesContext_Stub : GroveAirlinesContext  // use it in its place
    {
        public GroveAirlinesContext_Stub(DbContextOptions<GroveAirlinesContext> options) : base(options)
        {

        }
    }
}
