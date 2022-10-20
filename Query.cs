namespace GraphQLDemo
{
    public class Query
    {
        [UseDbContext(typeof(SomeDbContext))]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<User> Users([ScopedService] SomeDbContext ctx)
        {
            return ctx.Users;
        }

        [UseDbContext(typeof(SomeDbContext))]
        [NodeResolver]
        [UseFirstOrDefault]
        [UseProjection]
        public IQueryable<User> UserById(int id,[ScopedService] SomeDbContext ctx)
        {
            return ctx.Users.Where(x => x.Id == id);
        }
    }
}
