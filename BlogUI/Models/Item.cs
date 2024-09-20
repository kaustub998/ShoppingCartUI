using BlazorBootstrap;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcorpUI.Models
{
    public class Item
    {
        public int itemId { get; set; }
        public int? userId { get; set; }
        public string? itemName { get; set; }
        public string? itemDescription { get; set; }
        public int? itemQuantity { get; set; }
        public decimal itemRate { get; set; }
        public bool isDeleted { get; set; }
        public List<ItemImage>? itemImageList { get; set; }
        public string? user_FullName { get; set; }
    }
   
    public class ItemImage
    {
        public int imageId { get; set; }
        public int? itemId { get; set; }
        public bool isDeleted { get; set; }
        public byte[]? image { get; set; }
    }
}
