import type { UserPaymentMethodModel } from "@/models/UserPaymentMethodModel";
export interface BankModel {
  bankId: number;
  bankName: string;
  bankStatus: boolean;
  userPaymentMethods: UserPaymentMethodModel[] | [];
}
