import type { EInvoicePaymentType } from "@/enums/EInvoicePaymentType";
import type { CustomerModel } from "@/models/CustomerModel";
import type { DetailInvoiceModel } from "@/models/DetailInvoiceModel";
import type { EmployeeModel } from "@/models/EmployeeModel";
import type { InvoiceDiscountModel } from "@/models/InvoiceDiscountModel";
import type { UserPaymentMethodModel } from "@/models/UserPaymentMethodModel";
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
