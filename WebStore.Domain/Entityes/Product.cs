using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebStore.Domain.Entityes.Base.Interfaces;

namespace WebStore.Domain.Entityes
{
    [Table("Products")]
    public class Product :INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public  int SectionId { get; set; }

        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }
        public int? BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }
        public string ImageUrl { get; set; }
        public  decimal Price { get; set; }
    }
}
