﻿using WebStore.Domain.Entities;

namespace WebStore.ViewModels
{
    public class BreadcrumbsViewModel
    {
        public Section Section { get; set; }

        public Brand Brand { get; set; }

        public string Product { get; set; }
    }
}
