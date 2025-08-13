using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class CustomerAddressModel : IGetIdEntity<uint>
    {
        public uint CustomerAddressId { get; private set; }
        public uint CustomerCityId { get; private set; }
        public uint CustomerDistrictId { get; private set; }
        public uint CustomerWardId { get; private set; }
        public uint CustomerId { get; private set; }
        public string CustomerStreet { get; private set; } = string.Empty;
        public string CustomerAddressNumber { get; private set; } = string.Empty;
        public bool CustomerAddressStatus { get; private set; } = true;

        // Navigation properties (nullable if not always loaded)
        public LocationCityModel? City { get; private set; }
        public LocationDistrictModel? District { get; private set; }
        public LocationWardModel? Ward { get; private set; }
        public CustomerModel? Customer { get; private set; }

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

        public uint GetIdEntity() => CustomerAddressId;
    }
}
