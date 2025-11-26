using BookerAPI;

namespace BookerEngineUnitTests.Stubs
{
    internal class DatabaseNotImplementedStub : IDatabase
    {
        public bool CheckIfBookingExists(string who, DateTime when)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfBookingExists(int bookingId)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfBookingPaidFully(int bookingId)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfDateFree(DateTime when)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public int SaveBooking(string who, DateTime when)
        {
            throw new NotImplementedException();
        }

        public int SavePayment(int bookingId)
        {
            throw new NotImplementedException();
        }
    }
}
