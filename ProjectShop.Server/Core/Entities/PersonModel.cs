using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public abstract class PersonModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _accountId;
        private DateTime _birthday;
        private string _phone = string.Empty;
        private string _email = string.Empty;
        private string _name = string.Empty;
        private string _avatarUrl = string.Empty;
        private bool _gender;
        private bool _status;

        // Các thuộc tính chung cho cả nhân viên và khách hàng.
        // Tên thuộc tính được đặt chung để dễ quản lý.
        public uint AccountId
        {
            get => _accountId;
            set => _accountId = value;
        }

        public DateTime Birthday
        {
            get => _birthday;
            set => _birthday = value;
        }

        public string Phone
        {
            get => _phone;
            set => _phone = value ?? string.Empty;
        } // Số điện thoại có thể để trống nếu không cần thiết.

        public string Email
        {
            get => _email;
            set => _email = value ?? string.Empty;
        } // Email có thể để trống nếu không cần thiết.

        public string Name
        {
            get => _name;
            set => _name = value ?? string.Empty;
        } // Tên có thể để trống nếu không cần thiết.

        public string AvatarUrl
        {
            get => _avatarUrl;
            set => _avatarUrl = value ?? string.Empty;
        } // Đường dẫn đến ảnh đại diện, có thể để trống nếu không có.

        public bool Gender
        {
            get => _gender;
            set => _gender = value;
        }

        public bool Status
        {
            get => _status;
            set => _status = value;
        }

        // Navigation properties
        public AccountModel Account { get; set; } = null!;
        public LocationWardModel? Ward { get; set; }
        public LocationDistrictModel? District { get; set; }
        public LocationCityModel? City { get; set; }
        // End of navigation properties

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
