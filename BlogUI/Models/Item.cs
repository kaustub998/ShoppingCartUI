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

    public class ConfirmedOrder
    {
        public int confirmedOrderId { get; set; }
        public int? buyerId { get; set; }
        public int? itemId { get; set; }
        public decimal? rate { get; set; }
        public int? quantity { get; set; }
        public DateTime? boughtDate { get; set; }
        public string? transactionId { get; set; }
        public decimal? total { get; set; }
        public string? buyerName { get; set; }
        public string? itemName { get; set; }
        public List<ItemImage>? itemImageList { get; set; }
    }
}
