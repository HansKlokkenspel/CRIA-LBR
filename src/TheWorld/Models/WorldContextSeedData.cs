using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace TheWorld.Models
{
    public class WorldContextSeedData
    {
        private WorldContext _context;
        private UserManager<WorldUser> _userManager;

        public WorldContextSeedData(WorldContext context, UserManager<WorldUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("hans.klokkenspel@theworld.com") == null)
            {
                var newUser = new WorldUser()
                {
                    UserName = "HansKlokkenspel",
                    Email = "hans.klokkenspel@theworld.com"
                };

                await _userManager.CreateAsync(newUser, "password");
            }

            if (!_context.Trips.Any())
            {
                var usTrip = new Trip()
                {
                    Name = "Us Trip",
                    Created = DateTime.Now,
                    UserName = "HansKlokkenspel",
                    Stops = new List<Stop>()
                    {
                        new Stop() { Name = "Atlanta, GA", Arrival = new DateTime(2015, 6, 4), Latitude = 33.748995, Longitude = -84.387982, Order = 0},
                        new Stop() { Name = "New York, NY", Arrival = new DateTime(2015, 7, 5), Latitude = 40.712784, Longitude = -74.005941, Order = 1},
                        new Stop() { Name = "Boston, MA", Arrival = new DateTime(2015, 7, 20), Latitude = 42.360082, Longitude = -71.058880, Order = 2},
                        new Stop() { Name = "Chicago, IL", Arrival = new DateTime(2015, 7, 27), Latitude = 41.878114, Longitude = -87.629798, Order = 3},
                        new Stop() { Name = "Seattle, WA", Arrival = new DateTime(2015, 8, 4), Latitude = 47.606209, Longitude = -122.332071, Order = 4},
                        new Stop() { Name = "Atlanta, GA", Arrival = new DateTime(2015, 8, 20), Latitude = 33.748995, Longitude = -84.387982, Order = 5}
                    }
                };

                _context.Trips.Add(usTrip);
                _context.Stops.AddRange(usTrip.Stops);

                _context.SaveChanges();
            }
        }
    }
}
