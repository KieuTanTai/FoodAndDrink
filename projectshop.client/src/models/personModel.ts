import { AccountModel } from "./accountModel";
import { LocationCityModel } from "./locationCityModel";
import { LocationDistrictModel } from "./locationDistrictModel";
import { LocationWardModel } from "./locationWardModel";

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