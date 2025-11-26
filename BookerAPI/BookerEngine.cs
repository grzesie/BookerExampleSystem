namespace BookerAPI
{
    public class BookerEngine
    {
        public const int ErrorId = -1;
        private readonly IDatabase _database;

        public BookerEngine(IDatabase database)
        {
            if (database == null) throw new ArgumentNullException(nameof(database), "Cannot pass empty DB");

            if (!database.IsConnected()) throw new Exception("DB not connected");

            this._database = database;
        }

        public int BookEvent(string who, DateTime when)
        {
            if (string.IsNullOrEmpty(who)) return ErrorId;

            if (when <= DateTime.Now) return ErrorId;

            if (_database.CheckIfBookingExists(who, when)) return ErrorId;

            if (!_database.CheckIfDateFree(when)) return ErrorId;

            return _database.SaveBooking(who, when);
        }

        public int PayForBooking(int bookId) { throw new NotImplementedException(); }
    }
}
