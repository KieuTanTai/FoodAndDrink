import type{ UserPaymentMethodModel } from './user-payment-method-model';
export interface BankModel {
    bankId: number;
    bankName: string;
    bankStatus: boolean;
    userPaymentMethods: UserPaymentMethodModel[] | [];
}