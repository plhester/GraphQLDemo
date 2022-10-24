using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GraphQLDemo
{
    public class User
    {
        public User()
        {
            Addresses = new HashSet<UserAddress>();
        }
        [GraphQLDescription("The User's internal Id")]
        public int Id { get; set; }
        [GraphQLDescription("The User's name")]
        public string Name { get; set; }
        [UsePaging]   //TODO: This doesn't seem to work! Cause HC exceptions for some reason!
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Returns a Connection containing a list of Addresses for the User that can easily be sorted, filtered, or paginated in any direction.")]
        public virtual ICollection<UserAddress> Addresses { get; set; }
    }
}
