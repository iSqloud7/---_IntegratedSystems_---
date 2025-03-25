using EShop.Web.Models.Identity;

namespace EShop.Web.Models.Domain
{
    public class ShoppingCart
    {
        public Guid ID { get; set; }
        public string? OwnerID { get; set; }
        public EShopApplicationUser? Owner { get; set; }
        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
    }
}
