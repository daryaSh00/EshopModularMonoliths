namespace Catalog.Products.Models
{
    /// <summary>
    /// Add a Create method for initializing Product entities
    /// Make property setters private to enforce encapsulation ?
    /// Add an Update method for modifying Product entities
    /// </summary>
    public class Product : Aggregate<Guid>
    {
        public string Name { get; private set; } = default!;
        public List<string> Category { get; private set; } = new();
        public string Description { get; private set; } = default!;
        public string ImageFile { get; private set; } = default!;
        public decimal Price { get; private set; }

        public static Product Create(
            Guid id,
            string name,
            List<string> category,
            string description,
            decimal price,
            string imageFile)
        {
            //Validation
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            //Defina product
            var product = new Product
            {
                Id = id,
                Name = name,
                Description = description,
                Price = price,
                ImageFile = imageFile,
                Category = category
            };
            product.AddDomainEvent(new ProductCreatedEvent(product));
            return product;
        }

        public void Update(string name,
            List<string> category,
            string description,
            decimal price,
            string imageFile)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            //Update Product entity filelds
            Name = name;
            Category = category;
            Description = description;
            Price = price;
            ImageFile = imageFile;

            if(Price != price)
            {
                Price = price;
                AddDomainEvent(new ProductPriceChangedEvent(this));
            }
        }
    }
}


