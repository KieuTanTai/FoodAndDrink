import type { EProductUnit } from "@/enums/EProductUnit";
import type { DetailCartModel } from "@/models/DetailCartModel";
import type { DetailInventoryModel } from "@/models/DetailInventoryModel";
import type { DetailInventoryMovementModel } from "@/models/DetailInventoryMovementModel";
import type { DetailInvoiceModel } from "@/models/DetailInvoiceModel";
import type { DetailProductLotModel } from "@/models/DetailProductLotModel";
import type { DetailSaleEventModel } from "@/models/DetailSaleEventModel";
import type { DisposeProductModel } from "@/models/DisposeProductModel";
import type { ProductCategoriesModel } from "@/models/ProductCategoriesModel";
import type { ProductImageModel } from "@/models/ProductImageModel";
import type { SupplierModel } from "@/models/SupplierModel";
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
