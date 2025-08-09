// File: Customer.cs
namespace ProjectShop.Server.Core.Entities
{
    public class CustomerModel : PersonModel
    {
        public uint CustomerId { get; private set; }

        // Các navigation properties riêng của Customer
        public CartModel Cart { get; private set; } = null!;
        public PointWalletModel PointWallet { get; private set; } = null!;
        public ICollection<InvoiceModel> Invoices { get; private set; } = new List<InvoiceModel>();

        // Constructor sử dụng `base(...)` để gọi constructor của lớp cha.
        public CustomerModel(uint customerId, uint accountId, DateTime customerBirthday, string customerPhone, string customerName, string customerHouseNumber, string customerStreet,
            uint? customerWardId, uint? customerDistrictId, uint? customerCityId, string customerAvatarUrl, bool customerGender, bool customerStatus)
            : base(accountId, customerBirthday, customerPhone, customerName, customerHouseNumber, customerStreet, customerWardId, customerDistrictId, customerCityId, customerAvatarUrl, customerGender, customerStatus)
        {
            CustomerId = customerId;
        }

        // Triển khai phương thức trừu tượng từ lớp cha.
        public override uint GetIdEntity() => CustomerId;
    }
}

