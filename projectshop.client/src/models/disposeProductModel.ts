import { DisposeReasonModel } from "./disposeReasonModel";
import { EmployeeModel } from "./employeeModel";
import { LocationModel } from "./locationModel";
import { ProductModel } from "./productModel";

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