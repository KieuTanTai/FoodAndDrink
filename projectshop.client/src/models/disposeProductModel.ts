import type{ DisposeReasonModel } from "./disposeReasonModel";
import type{ EmployeeModel } from "./employeeModel";
import type{ LocationModel } from "./locationModel";
import type{ ProductModel } from "./productModel";

export interface DisposeProductModel {
    disposeProductId: number;
    productBarcode: number;
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