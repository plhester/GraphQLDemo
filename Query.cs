namespace GraphQLDemo
{
    [GraphQLDescription("Query is one of the root-level data-types in GraphQL. It defines the entry-points for data-fetching operations.")]
    public class Query
    {
        [UseDbContext(typeof(SomeDbContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Returns a Connection containing a list of Users that can easily be sorted, filtered, or paginated in any direction.")]
        public IQueryable<User> GetUsers([ScopedService] SomeDbContext ctx)
        {
            return ctx.Users;
        }

        [UseDbContext(typeof(SomeDbContext))]
        [UseProjection]
        [UseFirstOrDefault]
        [NodeResolver]
        [GraphQLDescription("Fetches a single User given its ID.")]
        public IQueryable<User> GetUser(int id,[ScopedService] SomeDbContext ctx)
        {
            return ctx.Users.Where(x => x.Id == id);
        }
    }
}
