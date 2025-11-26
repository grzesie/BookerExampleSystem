using BookerAPI.Database;

namespace BookerAPI
{
    public class BookerDatabase : IDatabase
    {
        private readonly AppDbContext _appDbContext;

        public BookerDatabase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool CheckIfBookingExists(string who, DateTime when)
        {
            return _appDbContext.Bookings.FirstOrDefault(b => b.Who.Equals(who) && b.When.Equals(when)) != null;
        }

        public bool CheckIfBookingExists(int bookingId)
        {
            return _appDbContext.Bookings.FirstOrDefault(b => b.Id == bookingId) != null;
        }

        public bool CheckIfBookingPaidFully(int bookingId)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfDateFree(DateTime when)
        {
            return _appDbContext.Bookings.FirstOrDefault(b => b.When.Equals(when)) == null;
        }

        public bool IsConnected()
        {
            return _appDbContext.Database.CanConnect();
        }

        public int SaveBooking(string who, DateTime when)
        {
            var newBooking = new Booking()
            {
                Who = who,
                When = when
            };
            _appDbContext.Bookings.Add(newBooking);
            _appDbContext.SaveChanges();
            return newBooking.Id;
        }

        public int SavePayment(int bookingId)
        {
            throw new NotImplementedException();
        }
    }
}
