import { UserPaymentMethodModel } from './userPaymentMethodModel';
export interface BankModel {
    bankId: number;
    bankName: string;
    bankStatus: boolean;
    userPaymentMethods: UserPaymentMethodModel[] | [];
}