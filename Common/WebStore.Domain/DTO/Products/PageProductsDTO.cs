using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.DTO.Products
{
   public record PageProductsDTO(IEnumerable<ProductDTO> Products, int TotalCount);
}
