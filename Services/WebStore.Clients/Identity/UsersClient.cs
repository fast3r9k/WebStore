using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Interfaces;

namespace WebStore.Clients.Identity
{
    public class UsersClient : BaseClient
    {
        public UsersClient(IConfiguration Configuration) : base(Configuration, WebApi.Identity.User)
        {
        }
    }
}
