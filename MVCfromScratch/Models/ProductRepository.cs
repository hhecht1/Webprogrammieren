using System.Xml.Linq;

namespace MVCfromScratch.Models
{
    public class ProductsRepository
    {
        private static List<Product> _products = new List<Product>()
        {
            new Product {ProductId = 1, CategoryId = 1, Name = "Iced Tea", Quantity = 5, Price = 1.49d},
            new Product {ProductId = 2, CategoryId = 1, Name = "Canada Dry", Quantity = 1, Price = 4.99d},
            new Product {ProductId = 3, CategoryId = 2, Name = "Whole Wheat Bread", Quantity = 9, Price = 2.49d},
            new Product {ProductId = 4, CategoryId = 2, Name = "White Bread", Quantity = 3, Price = 1.99d},
        };

        // Create
        public static void AddProduct(Product product)
        {
            var maxId = _products.Max(x => x.ProductId);
            product.ProductId = maxId + 1;
            _products.Add(product);
        }


        public static List<Product> GetProducts(bool loadCategory = false)
        {
            if (!loadCategory)
            {
                return _products;
            }

            if (_products != null && _products.Count > 0)
            {
                _products.ForEach(x =>
                {
                    if (x.CategoryId.HasValue)
                        x.Category = CategoriesRepository.GetCategoryById((int)x.CategoryId);
                });
            }

            return _products ?? new List<Product>();
        }
        public static Product? GetProductById(int productId, bool loadCategory = false)  // update
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                var prod = new Product      // update
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    Quantity = product.Quantity,
                    Price = product.Price
                };

                if (loadCategory && prod.CategoryId.HasValue)      // update
                {
                    prod.Category = CategoriesRepository.GetCategoryById(prod.CategoryId.Value);
                }
                return prod;  // update
            }

            return null;
        }

        // Update
        public static void UpdateProduct(int productId, Product product)
        {
            if (productId != product.ProductId) return;

            // var categoryToUpdate = GetCategoryById(categoryId);          // erzeugt eine neue Instanz!!!
            var productToUpdate = _products.FirstOrDefault(x => x.ProductId == productId);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.Price = product.Price;
            }
        }

        // Delete
        public static void DeleteProduct(int productId)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                _products.Remove(product);
            }

        }
    }
}