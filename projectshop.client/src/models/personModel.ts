import type { AccountModel } from "./accountModel";
import type { LocationCityModel } from "./locationCityModel";
import type { LocationDistrictModel } from "./locationDistrictModel";
import type { LocationWardModel } from "./locationWardModel";

export interface PersonModel {
    accountId: number;
    birthday: string;
    phone: string;
    email: string;
    name: string;
    avatarUrl: string;
    gender: boolean;
    status: boolean;
    account: AccountModel | null;
    ward: LocationWardModel | null;
    district: LocationDistrictModel | null;
    city: LocationCityModel | null;
}