using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entityes.Base;
using WebStore.Domain.Entityes.Base.Interfaces;

namespace WebStore.Domain.Entityes
{
    [Table("Sections")]
    public class Section : NamedEntity, IOrderedEntity
    {
        public  int Order { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public  virtual  Section ParSection { get; set; }
        public  virtual  ICollection<Product> Products { get; set; }
    }
}
