import type { DisposeReasonModel } from "@/models/DisposeReasonModel";
import type { EmployeeModel } from "@/models/EmployeeModel";
import type { LocationModel } from "@/models/LocationModel";
import type { ProductModel } from "@/models/ProductModel";

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
