import type { InvoiceModel } from './InvoiceModel';
import type { CartModel } from "./CartModel";
import type { PersonModel } from "./PersonModel";
import type { CustomerAddressModel } from './CustomerAddressModel';

export interface CustomerModel {
    customerId: number;
    personIdRef: number;
    loyaltyPoints: number;
    registrationDate: string;
    person: PersonModel | null;
    cart: CartModel | null;
    invoices: InvoiceModel[] | [];
    customerAddresses: CustomerAddressModel[] | [];
}