import type { BankModel } from "./bank-model";
import type { CustomerModel } from "./customer-model";
import type { InvoiceModel } from "./invoice-model";

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