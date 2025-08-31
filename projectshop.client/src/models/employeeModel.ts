import { LocationCityModel } from "./locationCityModel";
import { LocationDistrictModel } from "./locationDistrictModel";
import { LocationModel } from "./locationModel";
import { LocationWardModel } from "./locationWardModel";
import { PersonModel } from "./personModel";

export interface EmployeeModel extends PersonModel {
    employeeId: number;
    employeeHouseNumber: string;
    employeeStreet: string;
    employeeWardId: number | null;
    employeeDistrictId: number | null;
    employeeCityId: number | null;
    locationId: number;
    employeeWard: LocationWardModel | null;
    employeeDistrict: LocationDistrictModel | null;
    employeeCity: LocationCityModel | null;
    location: LocationModel | null;
}