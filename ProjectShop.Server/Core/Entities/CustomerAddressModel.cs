using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class CustomerAddressModel : IGetIdEntity<uint>
    {
        public uint CustomerAddressId { get; set; }
        public uint CustomerCityId { get; set; }
        public uint CustomerDistrictId { get; set; }
        public uint CustomerWardId { get; set; }
        public uint CustomerId { get; set; }
        public string CustomerStreet { get; set; } = string.Empty;
        public string CustomerAddressNumber { get; set; } = string.Empty;
        public bool CustomerAddressStatus { get; set; } = true;

        // Navigation properties
        public LocationCityModel? City { get; set; }
        public LocationDistrictModel? District { get; set; }
        public LocationWardModel? Ward { get; set; }
        public CustomerModel? Customer { get; set; }
        // End of navigation properties

        public CustomerAddressModel(
            uint customerAddressId,
            uint customerCityId,
            uint customerDistrictId,
            uint customerWardId,
            uint customerId,
            string customerStreet,
            string customerAddressNumber,
            bool customerAddressStatus,
            LocationCityModel? city = null,
            LocationDistrictModel? district = null,
            LocationWardModel? ward = null,
            CustomerModel? customer = null
        )
        {
            CustomerAddressId = customerAddressId;
            CustomerCityId = customerCityId;
            CustomerDistrictId = customerDistrictId;
            CustomerWardId = customerWardId;
            CustomerId = customerId;
            CustomerStreet = customerStreet;
            CustomerAddressNumber = customerAddressNumber;
            CustomerAddressStatus = customerAddressStatus;
            City = city;
            District = district;
            Ward = ward;
            Customer = customer;
        }

        public CustomerAddressModel() { }

        public uint GetIdEntity() => CustomerAddressId;
    }
}
