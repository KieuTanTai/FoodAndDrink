import type{ EInvoicePaymentType } from './../enums/eInvoicePaymentType';
import type{ CustomerModel } from './customerModel';
import type{ DetailInvoiceModel } from './detailInvoiceModel';
import type{ EmployeeModel } from './employeeModel';
import type{ InvoiceDiscountModel } from './invoiceDiscountModel';
import type{ UserPaymentMethodModel } from './userPaymentMethodModel';
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