using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GraphQLDemo
{
    public class User
    {
        public User()
        {
            Addresses = new HashSet<UserAddress>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public virtual ICollection<UserAddress> Addresses { get; set; }
    }
}
