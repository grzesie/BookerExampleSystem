namespace BookerAPI.Database
{
    public class Booking
    {
        public int Id { get; init; }
        public string Who { get; set; }
        public DateTime When { get; set; }
    }
}
