using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entityes.Base.Interfaces;

namespace WebStore.Domain.Entityes.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
