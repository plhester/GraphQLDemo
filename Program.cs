using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                // First we add the DBContext which we will be using to interact with our
                // Database.
                .AddPooledDbContextFactory<SomeDbContext>(o =>
                    o.UseSqlite(builder.Configuration.GetConnectionString("default")))

                // This adds the GraphQL server core service and declares a schema.
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .SetPagingOptions(new PagingOptions
                {
                    MaxPageSize = 1000,
                    DefaultPageSize = 100,
                    IncludeTotalCount = true,
                })
                .AddGlobalObjectIdentification()
                .InitializeOnStartup()
                .RegisterDbContext<SomeDbContext>(DbContextKind.Pooled)
                ;

            var app = builder.Build();
            app
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            })
            ;

            await SeedDataAsync(app);

            await app.RunAsync();
        }

        public static async Task SeedDataAsync(WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            IDbContextFactory<SomeDbContext> dbContextFactory = scope.ServiceProvider.GetService<IDbContextFactory<SomeDbContext>>();
            using SomeDbContext context = dbContextFactory!.CreateDbContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            if (!await context.Users.AnyAsync())
            {
                context.Users.AddRange(
                    new User { Name = "Paul Hester" },
                    new User { Name = "Michael Staib" }
                    );

                await context.SaveChangesAsync();

                context.Addresses.AddRange(
                    new UserAddress { UserId = 1, Email = "plhester@msn.com" },
                    new UserAddress { UserId = 1, Email = "plhester@gmail.com" },
                    new UserAddress { UserId = 1, Email = "plhester@interplat.com" },
                    new UserAddress { UserId = 1, Email = "paul.hester@spectrummedical.com" },
                    new UserAddress { UserId = 2, Email = "michael@chillicream.com" },
                    new UserAddress { UserId = 2, Email = "michael.staib@hotmail.com" }
                    );

                await context.SaveChangesAsync();

                context.ChangeTracker.Clear();
            }
        }
    }
}