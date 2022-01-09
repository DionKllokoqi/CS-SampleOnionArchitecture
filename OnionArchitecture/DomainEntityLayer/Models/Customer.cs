namespace DomainEntityLayer.Models
{
    public class Customer : BaseEntity
    {
        public string CustomerName { get; set; }
        public string PurchasedProduct { get; set; }
        public string PaymentType { get; set; }
    }
}
