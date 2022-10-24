namespace GraphQLDemo
{
    public class UserAddress
    {
        public UserAddress()
        {

        }
        [GraphQLDescription("The internal Id for the Address")]
        public int Id { get; set; }
        [GraphQLDescription("The internal Id for the User")]
        public int UserId { get; set; }
        [GraphQLDescription("The Email address for the User")]
        public string Email { get; set; }
    }
}
