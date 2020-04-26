using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.ProductsStore.Data
{
    public interface IProductStore
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int Id);
        Task<ILookup<int, ChildProducts>> GetChildren(IEnumerable<int> productIds);
        Task<IEnumerable<ChildProducts>> GetChildren(int Id);
    }


    public class ProductStore : IProductStore
    {
        static List<Product> _products = new List<Product>();

        static ProductStore()
        {
            for (int i = 1; i <= 10; i++)
            {
                var name = "P" + i.ToString();
                var product = new Product() { Id = i, Name = name, Description = name + " product", Price = 10 * i };
                for (int j = 1; j <= 3; j++)
                {
                    var pc = name + " child " + j.ToString();
                    product.AssociatedProducts.Add(new ChildProducts() { Id = j, ParenetProductId=product.Id, Name = pc, Description = pc + " product", Price = 5 * j });

                }
                _products.Add(product);
            }
        }

        public async Task<IEnumerable<ChildProducts>> GetChildren(int Id)
        {
            return _products.FirstOrDefault(p => p.Id == Id)?.AssociatedProducts;
        }


        public async Task<ILookup<int, ChildProducts>> GetChildren(IEnumerable<int> productIds)
        {
            var childProducts = _products.FirstOrDefault(p => productIds.Contains(p.Id))?.AssociatedProducts;
            return childProducts.ToLookup(r => r.ParenetProductId);
        }



        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetProductById(int Id)
        {
            return _products.FirstOrDefault(p => p.Id == Id);
        }

    }


    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public List<ChildProducts> AssociatedProducts { get; set; }

        public Product()
        {
            AssociatedProducts = new List<ChildProducts>();
        }


    }

    public class ChildProducts
    {
        public int Id { get; set; }

        public int ParenetProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
