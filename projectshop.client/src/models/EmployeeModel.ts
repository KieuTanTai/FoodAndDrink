import type { LocationCityModel } from "./LocationCityModel";
import type { LocationDistrictModel } from "./LocationDistrictModel";
import type { LocationModel } from "./LocationModel";
import type { LocationWardModel } from "./LocationWardModel";
import type { PersonModel } from "./PersonModel";

export interface EmployeeModel {
    employeeId: number;
    personIdRef: number;
    employeeHouseNumber: string;
    employeeStreet: string;
    employeeWardId: number | null;
    employeeDistrictId: number | null;
    employeeCityId: number | null;
    locationId: number;
    hireDate: string;
    salary: number;
    person: PersonModel | null;
    employeeWard: LocationWardModel | null;
    employeeDistrict: LocationDistrictModel | null;
    employeeCity: LocationCityModel | null;
    location: LocationModel | null;
}