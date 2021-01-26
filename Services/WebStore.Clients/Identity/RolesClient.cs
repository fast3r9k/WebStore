﻿using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Interfaces;

namespace WebStore.Clients.Identity
{
    public class RolesClient : BaseClient
    {
        public RolesClient(IConfiguration Configuration) : base(Configuration,WebApi.Identity.Role)
        {
        }
    }
}