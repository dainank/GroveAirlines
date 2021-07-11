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
            base.Database.EnsureDeleted();  // IMPORTANT
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) // Look Into
        {
            IEnumerable<EntityEntry> pendingChanges = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            IEnumerable<Booking> bookings = pendingChanges.Select(e => e.Entity).OfType<Booking>();
            if (bookings.Any(b => b.CustomerId != 1))
            {
                throw new Exception("Database Error!");
            }

            IEnumerable<Airport> airports = pendingChanges.Select(e => e.Entity).OfType<Airport>();
            if (airports.Any(a => a.AirportId == 10))
            {
                throw new Exception("Database Error!");
            }

            IEnumerable<Customer> customers = pendingChanges.Select(e => e.Entity).OfType<Customer>();
            if (customers.Any(a => a.CustomerId == 10))
            {
                throw new Exception("Database Error!");
            }

            await base.SaveChangesAsync(cancellationToken);
            return 1;
        }
        }
    }
