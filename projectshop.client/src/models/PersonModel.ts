import type { AccountModel } from "./AccountModel";

export interface PersonModel {
    personId: number;
    accountId: number;
    birthday: string;
    phone: string;
    email: string;
    name: string;
    avatarUrl: string;
    gender: boolean;
    status: boolean;
    createdDate: string;
    lastUpdatedDate: string;
    account: AccountModel | null;
}