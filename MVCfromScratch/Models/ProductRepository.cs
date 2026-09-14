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
            new Product {ProductId = 4, CategoryId = 2, Name = "White Bread", Quantity = 0, Price = 1.99d},
            new Product {ProductId = 5, CategoryId = 1, Name = "Orange Juice", Quantity = 6, Price = 2.29d},
            new Product {ProductId = 6, CategoryId = 1, Name = "Lemonade", Quantity = 3, Price = 1.79d},
            new Product {ProductId = 7, CategoryId = 2, Name = "Bagels", Quantity = 8, Price = 3.19d},
            new Product {ProductId = 8, CategoryId = 2, Name = "Croissant", Quantity = 4, Price = 2.99d},
            new Product {ProductId = 9, CategoryId = 1, Name = "Sparkling Water", Quantity = 10, Price = 1.29d},
            new Product {ProductId = 10, CategoryId = 2, Name = "Multigrain Bread", Quantity = 7, Price = 2.79d},
            new Product {ProductId = 11, CategoryId = 1, Name = "Apple Juice", Quantity = 5, Price = 2.19d},
            new Product {ProductId = 12, CategoryId = 2, Name = "Pita Bread", Quantity = 6, Price = 1.89d},
            new Product {ProductId = 13, CategoryId = 1, Name = "Energy Drink", Quantity = 2, Price = 3.49d},
            new Product {ProductId = 14, CategoryId = 2, Name = "Baguette", Quantity = 5, Price = 2.69d}

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