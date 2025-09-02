import type{ InvoiceModel } from './invoice-model';
import type{ CartModel } from "./cart-model";
import type{ PersonModel } from "./person-model";
import type { CustomerAddressModel } from './customer-address-model';

export interface CustomerModel extends PersonModel {
    customerId: number;
    cart: CartModel | null;
    invoices: InvoiceModel[] | [];
    customerAddresses: CustomerAddressModel[] | [];
}