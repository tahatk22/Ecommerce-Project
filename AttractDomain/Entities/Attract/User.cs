using Microsoft.AspNetCore.Identity;

namespace AttractDomain.Entities.Attract
{
    public class User:IdentityUser
    {
        public User()
        {
            Bills = new HashSet<Bill>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public ICollection<Bill> Bills { get; set; }
    }
}
