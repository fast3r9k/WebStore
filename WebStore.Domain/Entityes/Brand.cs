using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entityes.Base;
using WebStore.Domain.Entityes.Base.Interfaces;

namespace WebStore.Domain.Entityes
{
    [Table("Brands")]
    public class Brand : NamedEntity, IOrderedEntity
    {

        public int Order { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
