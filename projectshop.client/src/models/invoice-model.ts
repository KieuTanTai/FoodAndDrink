import type{ EInvoicePaymentType } from './../enums/e-invoice-payment-type';
import type{ CustomerModel } from './customer-model';
import type{ DetailInvoiceModel } from './detail-invoice-model';
import type{ EmployeeModel } from './employee-model';
import type{ InvoiceDiscountModel } from './invoice-discount-model';
import type{ UserPaymentMethodModel } from './user-payment-method-model';
export interface InvoiceModel {
    invoiceId: number;
    customerId: number;
    employeeId: number;
    paymentMethodId: number;
    invoiceTotalPrice: number;
    invoiceDate: string;
    invoiceStatus: boolean;
    paymentType: EInvoicePaymentType;
    customer: CustomerModel | null;
    employee: EmployeeModel | null;
    userPaymentMethod: UserPaymentMethodModel | null;
    detailInvoices: DetailInvoiceModel[] | [];
    invoiceDiscounts: InvoiceDiscountModel[] | [];
}