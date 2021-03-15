namespace PublicApi.DTO.v1
{
    public class OrderRow
    {
        public string ProductName { get; set; } = default!;
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}