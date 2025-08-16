using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public abstract class PersonModel : IGetIdEntity<uint>
    {
        // Các thuộc tính chung cho cả nhân viên và khách hàng.
        // Tên thuộc tính được đặt chung để dễ quản lý.
        public uint AccountId { get; set; }

        public DateTime Birthday { get; set; }
        public string Phone { get; set; } = string.Empty; // Số điện thoại có thể để trống nếu không cần thiết.

        public string Email { get; set; } = string.Empty; // Email có thể để trống nếu không cần thiết.
        public string Name { get; set; } = string.Empty; // Tên có thể để trống nếu không cần thiết.

        public string AvatarUrl { get; set; } = string.Empty; // Đường dẫn đến ảnh đại diện, có thể để trống nếu không có.
        public bool Gender { get; set; }
        public bool Status { get; set; }

        // Navigation properties
        public AccountModel Account { get; set; } = null!;
        public LocationWardModel? Ward { get; set; }
        public LocationDistrictModel? District { get; set; }
        public LocationCityModel? City { get; set; }

        // Constructor cho lớp cơ sở.
        public PersonModel(uint accountId, DateTime birthday, string phone, string name, string email, string avatarUrl, bool gender, bool status)
        {
            AccountId = accountId;
            Birthday = birthday;
            Phone = phone;
            Name = name;
            AvatarUrl = avatarUrl;
            Gender = gender;
            Status = status;
            Email = email;
        }

        public PersonModel()
        {
        }

        // Phương thức trừu tượng phải được triển khai bởi các lớp con.
        public abstract uint GetIdEntity();
    }
}
