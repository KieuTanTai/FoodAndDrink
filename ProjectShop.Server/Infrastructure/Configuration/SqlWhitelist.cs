using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class SqlWhitelist
    {
        private static readonly HashSet<string> AllowedColumnNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            // LOCATIONS
            "location_type_id", "location_type_name", "location_type_status",
            "location_city_id", "location_city_name", "location_city_status",
            "location_district_id", "location_district_name", "location_district_status",
            "location_ward_id", "location_ward_name", "location_ward_status",
            "location_street_id", "location_street_name", "location_street_status",
            "location_id", "address_number", "location_phone", "location_email", "location_name", "location_status",
            
            // USERS
            "supplier_id", "supplier_name", "supplier_phone", "supplier_email", "supplier_company_address_number",
            "supplier_company_street_id", "supplier_company_ward_id", "supplier_company_district_id",
            "supplier_company_city_id", "supplier_store_address_number", "supplier_store_street_id",
            "supplier_store_ward_id", "supplier_store_district_id", "supplier_store_city_id", "supplier_status",
            "account_id", "user_name", "password", "account_create_date", "account_status",
            "role_id", "role_name", "role_status",
            "customer_id", "customer_birthday", "customer_phone", "customer_name",
            "customer_address_number", "customer_street_id", "customer_ward_id", "customer_district_id",
            "customer_city_id", "customer_avatar_url", "customer_gender", "customer_status",
            "employee_id", "employee_birthday", "employee_phone", "employee_name",
            "employee_address_number", "employee_street_id", "employee_ward_id", "employee_district_id",
            "employee_city_id", "employee_avatar_url", "employee_gender", "employee_status",
            "id", "create_date",
            "cart_id", "cart_total_price",
            "point_wallet_id", "balance_point", "last_update_balance_date",

            // PRODUCTS
            "product_id", "product_name", "product_net_weight", "product_weight_range", "product_unit",
            "product_base_price", "product_rating_age", "product_status",
            "detail_cart_id", "cart_id", "product_id", "cart_add_date", "cart_price", "cart_quantity",
            "product_lot_id", "product_lot_mfg_date", "product_lot_exp_date", "product_lot_initial_quantity",
            "dispose_reason_id", "dispose_reason_name",
            "category_id", "category_name", "category_status",
            "product_image_id", "image_url", "product_image_priority",

            // SALE EVENTS
            "sale_event_id", "sale_event_start_date", "sale_event_end_date", "sale_event_name",
            "sale_event_status", "sale_event_description", "sale_event_discount_code",
            "detail_sale_event_id", "discount_type", "discount_percent", "discount_amount",
            "max_discount_price", "min_price_to_use",

            // NEWS
            "news_category_id", "news_category_name", "news_category_status",
            "news_id", "news_related_product_id", "news_title", "news_published_date",
            "news_content", "news_status",
            "news_image_id", "news_image_url",

            // PAYMENT & INVOICES
            "bank_id", "bank_name", "bank_status",
            "user_payment_method_id", "payment_method_type", "added_date", "last_updated_date",
            "status", "display_name", "last_four_digit", "expiry_year", "expiry_month", "token",
            "invoice_id", "payment_method_id", "invoice_total_price", "invoice_date", "invoice_status",
            "detail_invoice_id", "detail_invoice_quantity", "detail_invoice_price",
            "transaction_id", "point_wallet_id", "transaction_date", "transaction_type",
            "transaction_current_balance", "transaction_status",

            // INVENTORIES
            "inventory_id", "inventory_current_quantity",
            "inventory_movement_id", "source_location_id", "destination_location_id",
            "inventory_movement_quantity", "inventory_movement_date", "inventory_movement_reason",
            "dispose_product_id", "dispose_by_employee_id", "dispose_reason_id", "dispose_quantity"
        };

        private static readonly Regex ColumnNameRegex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);

        public static bool IsSafeColumn(string colName)
        {
            return ColumnNameRegex.IsMatch(colName) && AllowedColumnNames.Contains(colName);
        }
    }
}