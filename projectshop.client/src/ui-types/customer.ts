import type { AccountModel } from "../models/account-model";
import type { LocationCityModel } from "../models/location-city-model";
import type { LocationDistrictModel } from "../models/location-district-model";
import type { LocationWardModel } from "../models/location-ward-model";

export interface Customer {
     birthday: string;
     phone: string;
     email: string;
     name: string;
     avatarUrl: string;
     gender: boolean;
     account: AccountModel | null;
     ward: LocationWardModel | null;
     district: LocationDistrictModel | null;
     city: LocationCityModel | null;
}