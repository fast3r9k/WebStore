using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public const string Admin = "Admin";

        public const string DefaultADminPassword = "AdPass";

        public  string Description { get; set; }
    }
}
