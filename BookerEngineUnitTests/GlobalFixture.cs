namespace BookerEngineUnitTests
{
    using BookerAPI;
    using BookerAPI.Database;
    using DotNet.Testcontainers.Builders;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;
    using Testcontainers.PostgreSql;

    public class GlobalFixture : IAsyncLifetime
    {
        public PostgreSqlContainer Container { get; }
        public IServiceProvider ServiceProvider { get; private set; }
        public BookerEngine Engine { get; private set; }

        public GlobalFixture()
        {
            Container = new PostgreSqlBuilder()
                .WithImage("postgres:16")
                .WithDatabase("bookerdb")
                .WithUsername("test")
                .WithPassword("test")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(5432))
                .Build();
        }

        public async ValueTask InitializeAsync() 
        {
            await Container.StartAsync();

            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseNpgsql(Container.GetConnectionString());
            });

            // 3. Build service provider
            ServiceProvider = services.BuildServiceProvider();

            // 4. Automatyczne migracje
            using var scope = ServiceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await db.Database.MigrateAsync();
            // TODO: add DB tables creation

            // TODO: add proper initialization
            Engine = new BookerEngine(new BookerDatabase(db));
        }

        public async ValueTask DisposeAsync() => await Container.DisposeAsync();
    }

    [CollectionDefinition("Shared collection")]
    public class SharedCollectionDefinition : ICollectionFixture<GlobalFixture>
    {
    }
}
