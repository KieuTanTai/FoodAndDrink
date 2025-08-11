using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public abstract class PersonModel : IGetIdEntity<uint>
    {
        // Các thuộc tính chung cho cả nhân viên và khách hàng.
        // Tên thuộc tính được đặt chung để dễ quản lý.
        public uint AccountId { get; private set; }

        public DateTime Birthday { get; private set; }
        public string Phone { get; private set; }
        public string Name { get; private set; }

        public string HouseNumber { get; private set; }
        public string Street { get; private set; }

        // Các khóa ngoại và đối tượng điều hướng cho địa chỉ
        public uint? WardId { get; private set; }
        public uint? DistrictId { get; private set; }
        public uint? CityId { get; private set; }

        public string AvatarUrl { get; private set; }
        public bool Gender { get; private set; }
        public bool Status { get; private set; }

        // Navigation properties
        public AccountModel Account { get; private set; } = null!;
        public LocationWardModel? Ward { get; private set; }
        public LocationDistrictModel? District { get; private set; }
        public LocationCityModel? City { get; private set; }

        // Constructor cho lớp cơ sở.
        public PersonModel(uint accountId, DateTime birthday, string phone, string name, string houseNumber, string street, uint? wardId, uint? districtId, uint? cityId, string avatarUrl, bool gender, bool status)
        {
            AccountId = accountId;
            Birthday = birthday;
            Phone = phone;
            Name = name;
            HouseNumber = houseNumber;
            Street = street;
            WardId = wardId;
            DistrictId = districtId;
            CityId = cityId;
            AvatarUrl = avatarUrl;
            Gender = gender;
            Status = status;
        }

        // Phương thức trừu tượng phải được triển khai bởi các lớp con.
        public abstract uint GetIdEntity();
    }
}
