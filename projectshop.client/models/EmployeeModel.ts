import type { LocationCityModel } from "@/models/LocationCityModel";
import type { LocationDistrictModel } from "@/models/LocationDistrictModel";
import type { LocationModel } from "@/models/LocationModel";
import type { LocationWardModel } from "@/models/LocationWardModel";
import type { PersonModel } from "@/models/PersonModel";

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
