import type { BankModel } from "./bankModel";
import type { CustomerModel } from "./customerModel";
import type { InvoiceModel } from "./invoiceModel";

export interface UserPaymentMethodModel {
    userPaymentMethodId: number;
    paymentMethodType: string;
    bankId: number | null;
    customerId: number;
    paymentMethodAddedDate: string;
    paymentMethodLastUpdatedDate: string;
    paymentMethodStatus: number;
    paymentMethodDisplayName: string;
    paymentMethodLastFourDigit: string;
    paymentMethodExpiryYear: number;
    paymentMethod_ExpiryMonth: number;
    paymentMethodToken: string;
    bank: BankModel | null;
    customer: CustomerModel | null;
    invoices: InvoiceModel[] | [];
}