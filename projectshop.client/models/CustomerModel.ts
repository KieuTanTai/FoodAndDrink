import type { InvoiceModel } from "@/models/InvoiceModel";
import type { CartModel } from "@/models/CartModel";
import type { PersonModel } from "@/models/PersonModel";
import type { CustomerAddressModel } from "@/models/CustomerAddressModel";

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
