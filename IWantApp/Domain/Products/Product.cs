using Flunt.Validations;

namespace IWantApp.Domain.Products
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public bool HasStock { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public Product()
        {

        }

        public Product(string name, Category category, string description, bool hasStock, decimal price, string userId)
        {
            Name = name;
            Category = category;
            Description = description;
            HasStock = hasStock;
            Price = price;
            CreatedBy = userId;
            EditedBy = userId;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

            Validate();

        }

        private void Validate()
        {
            var contract = new Contract<Product>()
                .IsNotNullOrEmpty(Name, "Name")
                .IsGreaterOrEqualsThan(Name, 3, "Name")
                .IsNotNull(Category, "Category",  "Category not found")
                .IsNotNullOrEmpty(Description, "Description")
                .IsGreaterOrEqualsThan(Description, 3, "Description")
                .IsGreaterOrEqualsThan(Price, 0.01, "Price")
                .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
                .IsNotNullOrEmpty(EditedBy, "EditedBy");
            AddNotifications(contract);
        }
    }
}

