import type { LocationModel } from "../models/location-model";

export interface UIEmployee {
     birthday: string;
     phone: string;
     email: string;
     name: string;
     avatarUrl: string;
     employeeId: number;
     location: LocationModel | null;
}