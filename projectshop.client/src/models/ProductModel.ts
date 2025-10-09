import type { EProductUnit } from './../enums/e-product-unit';
import type { DetailCartModel } from './detail-cart-model';
import type { DetailInventoryModel } from './detail-inventory-model';
import type { DetailInventoryMovementModel } from './detail-inventory-movement-model';
import type { DetailInvoiceModel } from './detail-invoice-model';
import type { DetailProductLotModel } from './detail-product-lot-model';
import type { DetailSaleEventModel } from './detail-sale-event-model';
import type { DisposeProductModel } from './dispose-product-model';
import type { ProductCategoriesModel } from './product-categories-model';
import type { ProductImageModel } from './product-image-model';
import type { SupplierModel } from './supplier-model';
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