// File: Customer.cs
namespace ProjectShop.Server.Core.Entities
{
    public class CustomerModel : PersonModel
    {
        public uint CustomerId { get; set; }

        // Navigation properties
        public CartModel Cart { get; set; } = null!;
        public ICollection<InvoiceModel> Invoices { get; set; } = new List<InvoiceModel>();
        // End of navigation properties

        // Constructor sử dụng base(...) để gọi constructor của lớp cha PersonModel
        public CustomerModel(
            uint customerId,
            uint accountId,
            DateTime customerBirthday,
            string customerPhone,
            string customerName,
            string customerEmail,
            string customerAvatarUrl,
            bool customerGender,
            bool customerStatus)
            : base(accountId, customerBirthday, customerPhone, customerName, customerEmail, customerAvatarUrl, customerGender, customerStatus)
        {
            CustomerId = customerId;
        }

        public CustomerModel() : base() { }

        // Triển khai phương thức trừu tượng từ lớp cha.
        public override uint GetIdEntity() => CustomerId;
    }
}

