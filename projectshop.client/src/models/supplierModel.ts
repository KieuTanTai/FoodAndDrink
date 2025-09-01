import type { LocationModel } from "./locationModel";
import type { ProductModel } from "./productModel";

export interface SupplierModel {
    supplierId: number;
    supplierName: string;
    supplierPhone: string;
    supplierEmail: string;
    companyLocationId: number | null;
    storeLocationId: number | null;
    supplierStatus: boolean;
    supplierCooperationDate: string;
    storeLocation: LocationModel | null;
    companyLocation: LocationModel;
    products: ProductModel[] | [];
}