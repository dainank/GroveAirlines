using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GroveAirlines.DatabaseLayer;
using GroveAirlines.DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GroveAirlines_Tests.Stubs
{
    class GroveAirlinesContext_Stub : GroveAirlinesContext  // use it in its place
    {
        public GroveAirlinesContext_Stub(DbContextOptions<GroveAirlinesContext> options) : base(options)
        {

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry> pendingChanges = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            IEnumerable<Booking> bookings = pendingChanges.Select(e => e.Entity).OfType<Booking>();
            if (bookings.Any(b => b.CustomerId != 1))
            {
                throw new Exception("Database Error!");
            }

            await base.SaveChangesAsync(cancellationToken);
            return 1;
        }   
    }
}
