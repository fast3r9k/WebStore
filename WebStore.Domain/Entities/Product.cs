﻿using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    public class Product :INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public virtual Section Section { get; set; }
        public int? BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }
        public string ImageUrl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public  decimal Price { get; set; }
    }
}
