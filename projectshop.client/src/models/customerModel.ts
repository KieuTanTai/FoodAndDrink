import type{ InvoiceModel } from './invoiceModel';
import type{ CartModel } from "./cartModel";
import type{ PersonModel } from "./personModel";

export interface CustomerModel extends PersonModel {
    customerId: number;
    cart: CartModel | null;
    invoices: InvoiceModel[] | [];
}