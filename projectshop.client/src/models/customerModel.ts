import { InvoiceModel } from './invoiceModel';
import { CartModel } from "./cartModel";
import { PersonModel } from "./personModel";

export interface CustomerModel extends PersonModel {
    customerId: number;
    cart: CartModel | null;
    invoices: InvoiceModel[] | [];
}