import type { EProductUnit } from './../enums/eProductUnit';
import type { DetailCartModel } from './detailCartModel';
import type { DetailInventoryModel } from './detailInventoryModel';
import type { DetailInventoryMovementModel } from './detailInventoryMovementModel';
import type { DetailInvoiceModel } from './detailInvoiceModel';
import type { DetailProductLotModel } from './detailProductLotModel';
import type { DetailSaleEventModel } from './detailSaleEventModel';
import type { DisposeProductModel } from './disposeProductModel';
import type { ProductCategoriesModel } from './productCategoriesModel';
import type { ProductImageModel } from './productImageModel';
import type { SupplierModel } from './supplierModel';
export interface ProductModel {
    productBarcode: string;
    supplierId: number;
    productName: string;
    productNetWeight: number;
    productWeightRange: string;
    productUnit: EProductUnit;
    productBasePrice: number;
    productRatingAge: string;
    productStatus: boolean;
    productAddedDate: string;
    productLastUpdatedDate: string;
    supplier: SupplierModel | null;
    detailCarts: DetailCartModel[] | [];
    detailProductLot: DetailProductLotModel[] | [];
    productCategories: ProductCategoriesModel[] | [];
    productImages: ProductImageModel[] | [];
    detailSaleEvents: DetailSaleEventModel[] | [];
    detailInvoices: DetailInvoiceModel[] | [];
    // NEW
    detailInventoryMovements: DetailInventoryMovementModel[] | [];
    detailInventories: DetailInventoryModel[] | [];
    disposeProducts: DisposeProductModel[] | [];
    // End of navigation properties
}