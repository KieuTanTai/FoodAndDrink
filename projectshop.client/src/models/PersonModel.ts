import type { AccountModel } from "./account-model";
import type { LocationCityModel } from "./location-city-model";
import type { LocationDistrictModel } from "./location-district-model";
import type { LocationWardModel } from "./location-ward-model";

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