import { EProductUnit } from './../Enums/eProductUnit';
import { DetailCartModel } from './detailCartModel';
import { DetailInventoryModel } from './detailInventoryModel';
import { DetailInventoryMovementModel } from './detailInventoryMovementModel';
import { DetailInvoiceModel } from './detailInvoiceModel';
import { DetailProductLotModel } from './detailProductLotModel';
import { DetailSaleEventModel } from './detailSaleEventModel';
import { DisposeProductModel } from './disposeProductModel';
import { ProductCategoriesModel } from './productCategoriesModel';
import { ProductImageModel } from './productImageModel';
import { SupplierModel } from './supplierModel';
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