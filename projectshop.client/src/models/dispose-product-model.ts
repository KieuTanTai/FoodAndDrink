import type{ DisposeReasonModel } from "./dispose-reason-model";
import type{ EmployeeModel } from "./employee-model";
import type{ LocationModel } from "./location-model";
import type{ ProductModel } from "./product-model";

export interface DisposeProductModel {
    disposeProductId: number;
    productBarcode: string;
    locationId: number;
    disposeByEmployeeId: number;
    disposeReasonId: number;
    disposeQuantity: number;
    disposedDate: string;
    product: ProductModel | null;
    location: LocationModel | null;
    disposeByEmployee: EmployeeModel | null;
    disposeReason: DisposeReasonModel | null;
}