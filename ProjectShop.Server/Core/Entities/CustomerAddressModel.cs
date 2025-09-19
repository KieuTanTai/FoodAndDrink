using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class CustomerAddressModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _customerAddressId;
        private uint _customerCityId;
        private uint _customerDistrictId;
        private uint _customerWardId;
        private uint _customerId;
        private string _customerStreet = string.Empty;
        private string _customerAddressNumber = string.Empty;
        private bool _customerAddressStatus = true;

        public uint CustomerAddressId
        {
            get => _customerAddressId;
            set => _customerAddressId = value;
        }

        public uint CustomerCityId
        {
            get => _customerCityId;
            set => _customerCityId = value;
        }

        public uint CustomerDistrictId
        {
            get => _customerDistrictId;
            set => _customerDistrictId = value;
        }

        public uint CustomerWardId
        {
            get => _customerWardId;
            set => _customerWardId = value;
        }

        public uint CustomerId
        {
            get => _customerId;
            set => _customerId = value;
        }

        public string CustomerStreet
        {
            get => _customerStreet;
            set => _customerStreet = value ?? string.Empty;
        }

        public string CustomerAddressNumber
        {
            get => _customerAddressNumber;
            set => _customerAddressNumber = value ?? string.Empty;
        }

        public bool CustomerAddressStatus
        {
            get => _customerAddressStatus;
            set => _customerAddressStatus = value;
        }

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
