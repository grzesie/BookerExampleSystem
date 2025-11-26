using Microsoft.Extensions.DependencyInjection;
using BookerAPI;
using BookerAPI.Database;

namespace BookerEngineUnitTests
{
    [Collection("Shared collection")]
    public class EngineBookingUnitTests : IDisposable
    {
        private readonly GlobalFixture _fixture;
        private readonly ITestOutputHelper _output;
        private readonly IServiceScope _serviceScope;
        private readonly BookerEngine _bookerEngine;

        public EngineBookingUnitTests(GlobalFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;

            _serviceScope = _fixture.ServiceProvider.CreateScope();
            var db = _serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            _bookerEngine = new BookerEngine(new BookerDatabase(db));
        }

        [Fact]
        public void EmptyWhoTest()
        {
            Assert.Equal(BookerEngine.ErrorId, _bookerEngine.BookEvent("", DateTime.UtcNow.AddHours(1)));
        }

        [Fact]
        public void NullWhoTest()
        {
            Assert.Equal(BookerEngine.ErrorId, _bookerEngine.BookEvent(null, DateTime.UtcNow.AddHours(2)));
        }

        [Theory]
        [InlineData("Hello", "2025-02-15")]
        [InlineData("Test", "1999-01-01")]
        public void BookInPastTest(string who, DateTime when)
        {
            Assert.Equal(BookerEngine.ErrorId, _bookerEngine.BookEvent(who, when));
        }

        [Fact]
        public void SameTimeBookingTest()
        {
            var datetime = DateTime.UtcNow.AddHours(3);
            _output.WriteLine($"Used data = {datetime}");
            Assert.NotEqual(BookerEngine.ErrorId, _bookerEngine.BookEvent("User1", datetime));
            _output.WriteLine("1st event booked");
            Assert.Equal(BookerEngine.ErrorId, _bookerEngine.BookEvent("User2", datetime));
            _output.WriteLine("2nd event booked");
        }

        [Fact]
        public void DifferentBookingIdsTest()
        {
            var bookingId1 = _bookerEngine.BookEvent("User1", DateTime.UtcNow.AddHours(4));
            Assert.NotEqual(BookerEngine.ErrorId, bookingId1);
            var bookingId2 = _bookerEngine.BookEvent("User1", DateTime.UtcNow.AddHours(5));
            Assert.NotEqual(BookerEngine.ErrorId, bookingId2);
            Assert.NotEqual(bookingId1, bookingId2);
        }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}
