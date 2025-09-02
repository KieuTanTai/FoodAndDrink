import type { LocationModel } from "../models/locationModel";

export interface Employee {
     birthday: string;
     phone: string;
     email: string;
     name: string;
     avatarUrl: string;
     employeeId: number;
     location: LocationModel | null;
}