namespace BookerEngineUnitTests;

using BookerAPI;
using BookerEngineUnitTests.Stubs;

public class EngineCreationUnitTests
{
    private readonly ITestOutputHelper _output;

    public EngineCreationUnitTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void DatabaseNullTest()
    {
        _output.WriteLine("Creating engine with null DB");
        Assert.Throws<ArgumentNullException>(() => new BookerEngine(null));
    }

    [Fact]
    public void DatabaseNotConnectedTest()
    {
        Assert.Throws<Exception>(() => new BookerEngine(new DatabaseNotConnectedStub()));
    }

    [Fact]
    public void DatabaseConnectedTest()
    {
        new BookerEngine(new DatabaseConnectedStub());
    }
}
