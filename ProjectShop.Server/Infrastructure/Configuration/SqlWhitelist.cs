using System.Text.RegularExpressions;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class SqlWhitelist
    {
        private static readonly HashSet<string> AllowedColumnNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            // location_type
            "location_type_id",
            "location_type_name",
            "location_type_status",

            // location_city
            "location_city_id",
            "location_city_name",
            "location_city_status",

            // location_district
            "location_district_id",
            "location_district_name",
            "location_district_status",

            // location_ward
            "location_ward_id",
            "location_ward_name",
            "location_ward_status",

            // location
            "location_id",
            "location_type_id",
            "location_house_number",
            "location_street",
            "location_ward_id",
            "location_district_id",
            "location_city_id",
            "location_phone",
            "location_email",
            "location_name",
            "location_status",

            // supplier
            "supplier_id",
            "supplier_name",
            "supplier_phone",
            "supplier_email",
            "company_location_id",
            "store_location_id",
            "supplier_status",
            "supplier_cooperation_date",

            // account
            "account_id",
            "user_name",
            "password",
            "account_created_date",
            "account_last_updated_date",
            "account_status",

            // role
            "role_id",
            "role_name",
            "role_status",
            "role_created_date",
            "role_last_updated_date",

            // customer
            "customer_id",
            "account_id",
            "customer_birthday",
            "customer_phone",
            "customer_name",
            "customer_email",
            "customer_avatar_url",
            "customer_gender",
            "customer_status",

            // customer_address
            "customer_address_id",
            "customer_city_id",
            "customer_district_id",
            "customer_ward_id",
            "customer_id",
            "customer_street",
            "customer_address_number",
            "customer_address_status",

            // employee
            "employee_id",
            "account_id",
            "employee_birthday",
            "employee_phone",
            "employee_name",
            "employee_house_number",
            "employee_street",
            "employee_ward_id",
            "employee_district_id",
            "employee_city_id",
            "employee_avatar_url",
            "location_id",
            "employee_email",
            "employee_gender",
            "employee_status",

            // roles_of_user
            "id",
            "account_id",
            "role_id",
            "added_date",

            // cart
            "cart_id",
            "customer_id",
            "cart_total_price",

            // category
            "category_id",
            "category_name",
            "category_status",

            // product
            "product_barcode",
            "category_id",
            "supplier_id",
            "product_name",
            "product_net_weight",
            "product_weight_range",
            "product_unit",
            "product_base_price",
            "product_rating_age",
            "product_status",
            "product_added_date",
            "product_last_updated_date",

            // detail_cart
            "detail_cart_id",
            "cart_id",
            "product_barcode",
            "detail_cart_added_date",
            "detail_cart_price",
            "detail_cart_quantity",

            // dispose_reason
            "dispose_reason_id",
            "dispose_reason_name",

            // product_categories
            "id",
            "category_id",
            "product_barcode",

            // product_image
            "product_image_id",
            "product_barcode",
            "product_image_url",
            "product_image_priority",
            "product_image_created_date",
            "product_image_last_updated_date",

            // sale_event
            "sale_event_id",
            "sale_event_start_date",
            "sale_event_end_date",
            "sale_event_name",
            "sale_event_status",
            "sale_event_description",
            "sale_event_discount_code",

            // sale_event_image
            "sale_event_image_id",
            "sale_event_id",
            "sale_event_image_url",
            "sale_event_image_created_date",
            "sale_event_image_last_updated_date",

            // detail_sale_event
            "detail_sale_event_id",
            "sale_event_id",
            "product_barcode",
            "discount_type",
            "discount_percent",
            "discount_amount",
            "max_discount_price",
            "min_price_to_use",

            // bank
            "bank_id",
            "bank_name",
            "bank_status",

            // user_payment_method
            "user_payment_method_id",
            "payment_method_type",
            "bank_id",
            "customer_id",
            "payment_method_added_date",
            "payment_method_last_updated_date",
            "payment_method_status",
            "payment_method_display_name",
            "payment_method_last_four_digit",
            "payment_method_expiry_year",
            "payment_method_expiry_month",
            "payment_method_token",

            // inventory
            "inventory_id",
            "location_id",
            "inventory_status",
            "inventory_last_updated_date",

            // product_lot
            "product_lot_id",
            "inventory_id",
            "product_lot_created_date",

            // detail_product_lot
            "product_lot_id",
            "product_barcode",
            "product_lot_mfg_date",
            "product_lot_exp_date",
            "product_lot_initial_quantity",

            // product_lot_inventory
            "product_lot_id",
            "inventory_id",
            "product_lot_inventory_quantity",
            "product_lot_inventory_added_date",

            // detail_inventory
            "detail_inventory_id",
            "inventory_id",
            "product_barcode",
            "detail_inventory_quantity",
            "detail_inventory_added_date",
            "detail_inventory_last_updated_date",

            // inventory_movement
            "inventory_movement_id",
            "source_location_id",
            "destination_location_id",
            "inventory_id",
            "inventory_movement_quantity",
            "inventory_movement_date",
            "inventory_movement_reason",

            // detail_inventory_movement
            "detail_inventory_movement_id",
            "inventory_movement_id",
            "product_barcode",
            "detail_inventory_movement_quantity",

            // dispose_product
            "dispose_product_id",
            "location_id",
            "product_barcode",
            "dispose_by_employee_id",
            "dispose_reason_id",
            "dispose_quantity",
            "disposed_date",

            // invoice
            "invoice_id",
            "customer_id",
            "employee_id",
            "payment_method_id",
            "invoice_total_price",
            "invoice_date",
            "invoice_status",

            // detail_invoice
            "detail_invoice_id",
            "invoice_id",
            "product_barcode",
            "detail_invoice_quantity",
            "detail_invoice_price",
            "detail_invoice_status",

            // invoice_discount
            "invoice_id",
            "sale_event_id"
        };

        private static readonly Regex ColumnNameRegex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);

        public static bool IsSafeColumn(string colName)
        {
            return ColumnNameRegex.IsMatch(colName) && AllowedColumnNames.Contains(colName);
        }
    }
}