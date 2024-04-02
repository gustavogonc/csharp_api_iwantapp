using Flunt.Validations;

namespace IWantApp.Domain.Products
{
    public class Product : Entity
    {
        public Product()
        {
            
        }

        public Product(string name, Category category, string description, bool hasStock, string userId)
        {
            Name = name;
            Category = category;
            Description = description;
            HasStock = hasStock;

            CreatedBy = userId;
            EditedBy = userId;
            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;

            Validate();

        }

        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }  
        public string Description { get; set; }
        public bool HasStock { get; set; }
        public bool Active { get; set; }

        private void Validate()
        {
            var contract = new Contract<Product>()
                .IsNotNullOrEmpty(Name, "Name")
                .IsGreaterOrEqualsThan(Name, 3, "Name")
                 .IsNotNull(Category, "Category")
                 .IsNotNullOrEmpty(Description, "Description")
                 .IsGreaterOrEqualsThan(Description, 3, "Description")
                 .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
                 .IsNotNullOrEmpty(EditedBy, "EditedBy");
            AddNotifications(contract);
        }
    }
}

