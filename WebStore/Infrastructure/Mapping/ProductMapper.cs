﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product p) => new()
        {
            Id =p.Id,
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            Brand = p.Brand?.Name
        };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> p) => p.Select(ToView);
    }
}