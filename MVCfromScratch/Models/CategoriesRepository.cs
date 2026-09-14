namespace MVCfromScratch.Models
{
    public static class CategoriesRepository
    {
        private static List<Category> _categories = new List<Category>()
        {
            new Category { CategoryId = 1, Name = "Beverage", Description = "Beverage Description"},
            new Category { CategoryId = 2, Name = "Bakery", Description = "Bakery Description"},
            new Category { CategoryId = 3, Name = "Meat", Description = "Meat Description"},
            new Category { CategoryId = 4, Name = "Fruit", Description = "Fresh fruits and seasonal products"},
            new Category { CategoryId = 5, Name = "Vegetables", Description = "Fresh vegetables and greens"},
            new Category { CategoryId = 6, Name = "Dairy", Description = "Milk, cheese and dairy products"},
            new Category { CategoryId = 7, Name = "Snacks", Description = "Quick bites and savory snacks"},
            new Category { CategoryId = 8, Name = "Frozen", Description = "Frozen food and ice products"},
            new Category { CategoryId = 9, Name = "Cleaning", Description = "Household cleaning supplies"},
            new Category { CategoryId = 10, Name = "Personal Care", Description = "Care and hygiene products"},
            new Category { CategoryId = 11, Name = "Bread", Description = "Fresh bread and baked goods"},
            new Category { CategoryId = 12, Name = "Coffee", Description = "Coffee beans and coffee accessories"},
            new Category { CategoryId = 13, Name = "Tea", Description = "Tea leaves and hot beverages"},

        };


        // Create
        public static void AddCategory(Category category)
        {
            var maxId = _categories.Max(x => x.CategoryId);
            category.CategoryId = maxId + 1;
            _categories.Add(category);
        }

        // Read
        public static List<Category> GetCategories() => _categories;

        public static Category? GetCategoryById(int categoryId)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category != null)
            {
                return new Category
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description,
                };
            }

            return null;
        }

        // Update

        public static void UpdateCategory(int categoryId, Category category)
        {
            if (categoryId != category.CategoryId) return;

            // var categoryToUpdate = GetCategoryById(categoryId);          // erzeugt eine neue Instanz!!!
            var categoryToUpdate = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;
            }
        }

        // Delete
        public static void DeleteCategory(int categoryId)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category != null)
            {
                _categories.Remove(category);
            }

        }

    }
}