import { EInvoicePaymentType } from './../Enums/eInvoicePaymentType';
import { CustomerModel } from './customerModel';
import { DetailInvoiceModel } from './detailInvoiceModel';
import { EmployeeModel } from './employeeModel';
import { InvoiceDiscountModel } from './invoiceDiscountModel';
import { UserPaymentMethodModel } from './userPaymentMethodModel';
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