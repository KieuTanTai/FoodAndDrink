import type { AccountModel } from "../models/accountModel";
import type { LocationCityModel } from "../models/locationCityModel";
import type { LocationDistrictModel } from "../models/locationDistrictModel";
import type { LocationWardModel } from "../models/locationWardModel";

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