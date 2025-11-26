namespace BookerAPI
{
    public interface IDatabase
    {
        public bool IsConnected();
        public int SaveBooking(string who, DateTime when);
        public bool CheckIfBookingExists(string who, DateTime when);
        public bool CheckIfBookingExists(int bookingId);
        public bool CheckIfDateFree(DateTime when);
        public bool CheckIfBookingPaidFully(int bookingId);
        public int SavePayment(int bookingId);
    }
}
