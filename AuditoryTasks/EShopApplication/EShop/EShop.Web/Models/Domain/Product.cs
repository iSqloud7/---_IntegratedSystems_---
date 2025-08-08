using System.ComponentModel.DataAnnotations;

namespace EShop.Web.Models.Domain
{
    public class Product
    {
        public Guid ID { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public string? ProductImage { get; set; }
        [Required]
        public string? ProductDescription { get; set; }
        [Required]
        public int ProductPrice { get; set; }
        [Required]
        public int Rating { get; set; }
        public virtual ICollection<ProductInShoppingCart>? ProductInShoppingCarts { get; set; }
    }
}
