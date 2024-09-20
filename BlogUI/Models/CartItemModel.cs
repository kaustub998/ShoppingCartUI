using EcorpUI.Models;

namespace EcorpAPI.Models
{
    public class CartItemModel
    {
        public int cartItemId { get; set; }  
        public int? itemId { get; set; }      
        public int? userId { get; set; }       
        public int? quantity { get; set; }
        public string? itemName { get; set; }
        public string? itemDescription { get; set; }
        public decimal itemRate { get; set; }
        public List<ItemImage>? itemImageList { get; set; }
        public byte[]? image { get; set; }

    }
    

}
