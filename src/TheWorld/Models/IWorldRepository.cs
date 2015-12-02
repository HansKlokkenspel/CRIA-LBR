using System.Collections.Generic;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        IEnumerable<Trip> GetUserTripsWithStops(string name);

        Trip GetTripByName(string tripName, string username);

        void AddTrip(Trip newTrip);
        void AddStop(string tripName, Stop newStop, string username);
        bool SaveAll();
    }
}