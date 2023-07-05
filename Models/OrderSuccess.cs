namespace ShopCarrs.Models
{
    public class OrderSuccess
    {
        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; } 
        public string? Gender { get; set; }
        public DateTime? OrderDate { get; set; }

         public decimal? Total { get; set; }
    }
}
