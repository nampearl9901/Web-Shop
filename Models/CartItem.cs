namespace ShopCarrs.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal lineTotal { get; set; }
        public string ImagePath { get; set; }
    }
}
