import type { ProductModel } from "@/models/ProductModel";

export interface ProductImageModel {
  productImageId: number;
  productBarcode: string;
  productImageUrl: string;
  productImagePriority: number;
  productImageCreatedDate: string;
  productImageLastUpdatedDate: string;
  product: ProductModel | null;
}
