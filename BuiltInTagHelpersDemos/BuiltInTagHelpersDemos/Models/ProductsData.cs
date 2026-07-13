using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuiltInTagHelpersDemos.Models
{
    public class ProductsData
    {

        private List<Product> _Products = new List<Product>
        {
            new Product {
                ProductID = 1,
                ProductName = "AMD Ryzen 3990",
                Quantity=100,
                UnitsInStock=50,
                Discontinued = false,
                Cost=3000,
                LaunchDate=new DateTime(2019,10,1)
            },
            new Product {
                ProductID = 2,
                ProductName = "AMD Ryzen 3970",
                Quantity=100,
                UnitsInStock=70,
                Discontinued = false,
                Cost=2500,
                LaunchDate=new DateTime(2019,10,5)
            },
               new Product {
                ProductID = 3,
                ProductName = "AMD Ryzen 3960",
                Quantity=100,
                UnitsInStock=80,
                Discontinued = false,
                Cost=2000,
                LaunchDate=new DateTime(2019,10,15)
            },
                  new Product {
                ProductID = 4,
                ProductName = "AMD Ryzen 3950",
                Quantity=100,
                UnitsInStock=40,
                Discontinued = false,
                Cost=1500,
                LaunchDate=new DateTime(2019,10,25)
            }

        };

        public IEnumerable<Product> Products => _Products;

    }
}
