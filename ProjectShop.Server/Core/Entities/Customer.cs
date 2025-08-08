// File: Customer.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class Customer : IGetIdEntity<uint>
    {
        // Corresponds to 'customer_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint CustomerId { get; private set; }

        // Corresponds to 'account_id' (INT UNSIGNED UNIQUE)
        public uint AccountId { get; private set; }
        public Account Account { get; private set; } = null!;

        // Corresponds to 'customer_birthday' (DATE)
        public DateTime CustomerBirthday { get; private set; }

        // Corresponds to 'customer_phone' (VARCHAR(12))
        public string CustomerPhone { get; private set; }

        // Corresponds to 'customer_name' (NVARCHAR(100))
        public string CustomerName { get; private set; }

        // Corresponds to 'customer_house_number' (VARCHAR(20))
        public string CustomerHouseNumber { get; private set; }

        // Corresponds to 'customer_street' (NVARCHAR(40))
        public string CustomerStreet { get; private set; }

        // Corresponds to 'customer_ward_id' (INT UNSIGNED)
        public uint? CustomerWardId { get; private set; }
        public LocationWard? CustomerWard { get; private set; }

        // Corresponds to 'customer_district_id' (INT UNSIGNED)
        public uint? CustomerDistrictId { get; private set; }
        public LocationDistrict? CustomerDistrict { get; private set; }

        // Corresponds to 'customer_city_id' (INT UNSIGNED)
        public uint? CustomerCityId { get; private set; }
        public LocationCity? CustomerCity { get; private set; }

        // Corresponds to 'customer_avatar_url' (VARCHAR(255))
        public string CustomerAvatarUrl { get; private set; }

        // Corresponds to 'customer_gender' (TINYINT(1))
        public bool CustomerGender { get; private set; }

        // Corresponds to 'customer_status' (TINYINT(1))
        public bool CustomerStatus { get; private set; }

        // Navigation properties
        public ICollection<Cart> Carts { get; private set; } = new List<Cart>();
        public PointWallet PointWallet { get; private set; } = null!;
        public ICollection<Invoice> Invoices { get; private set; } = new List<Invoice>();

        public Customer(uint customerId, uint accountId, DateTime customerBirthday, string customerPhone, string customerName, string customerHouseNumber, string customerStreet, uint? customerWardId, uint? customerDistrictId, uint? customerCityId, string customerAvatarUrl, bool customerGender, bool customerStatus)
        {
            CustomerId = customerId;
            AccountId = accountId;
            CustomerBirthday = customerBirthday;
            CustomerPhone = customerPhone;
            CustomerName = customerName;
            CustomerHouseNumber = customerHouseNumber;
            CustomerStreet = customerStreet;
            CustomerWardId = customerWardId;
            CustomerDistrictId = customerDistrictId;
            CustomerCityId = customerCityId;
            CustomerAvatarUrl = customerAvatarUrl;
            CustomerGender = customerGender;
            CustomerStatus = customerStatus;
        }

        public uint GetIdEntity() => CustomerId;
    }
}

