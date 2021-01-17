using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public  const string Admin = "Admins";

        public  const string User = "Users";

        public string Description { get; set; }

    }
}