namespace Store.Domain.Entities
{
    public class Product : Entity
    {
        public Product(string? title, decimal price, bool activw)
        {
            Title = title;
            Price = price;
            Activw = activw;
        }

        public string? Title { get; private set; }
        public decimal Price { get; private set; }
        public bool Activw { get; private set; }
    }
}