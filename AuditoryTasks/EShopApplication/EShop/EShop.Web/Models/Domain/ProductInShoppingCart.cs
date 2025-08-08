namespace EShop.Web.Models.Domain
{
    public class ProductInShoppingCart
    {
        // public Guid ID { get; set; } // surrogate key
        public Guid ProductID { get; set; }
        public Product? Product { get; set; }
        public Guid ShoppingCartID { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
