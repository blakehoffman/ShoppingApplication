namespace Application.DTO.Discount
{
    public class CreateDiscountDTO
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public bool Active { get; set; }
    }
}
