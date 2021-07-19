using HouseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseApi.Database
{
    public interface IListingRepository
    {
        IList<Listing> GetListings();
        void Save();
    }

    /// <summary>
    /// Simulates an ORM like EntityFramework.
    /// </summary>
    public class InMemoryListingRepository : IListingRepository
    {
        private List<Listing> _listings = new List<Listing>();
        private List<Listing> _workingSet = new List<Listing>();

        public IList<Listing> GetListings()
        {
            _workingSet = _listings.ToList();
            return _workingSet;
        }

        public void Save()
        {
            // Enforce key uniqueness the way a relational DB might.
            var equalityComparer = new Listing.ListingEntityEqualityComparer();
            var duplicates = _workingSet
                .GroupBy(x => x, equalityComparer)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key);

            if (duplicates.Any())
            {
                throw new Exception("DB exception! Uniquess constraint violated.");
            }

            // "Commit" the changes
            _listings = _workingSet;
        }
    }
}
