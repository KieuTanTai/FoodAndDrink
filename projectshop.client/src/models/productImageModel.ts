import { ProductModel } from "./productModel";

export interface ProductImageModel {
    productImageId: number;
    productBarcode: string;
    productImageUrl: string;
    productImagePriority: number;
    productImageCreatedDate: string;
    productImageLastUpdatedDate: string;
    product: ProductModel | null;
}