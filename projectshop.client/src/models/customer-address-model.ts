import type{ LocationWardModel } from './location-ward-model';
import type{ LocationDistrictModel } from './location-district-model';
import type{ LocationCityModel } from './location-city-model';
import type{ CustomerModel } from './customer-model';
export interface CustomerAddressModel {
    customerAddressId: number;
    customerCityId: number;
    customerDistrictId: number;
    customerWardId: number;
    customerId: number;
    customerStreet: string;
    customerAddressNumber: string;
    customerAddressStatus: boolean;
    city: LocationCityModel | null;
    district: LocationDistrictModel | null;
    ward: LocationWardModel | null;
    customer: CustomerModel | null;
}