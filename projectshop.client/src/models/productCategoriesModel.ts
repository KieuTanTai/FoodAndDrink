import type { CategoryModel } from "./categoryModel";
import type { ProductModel } from "./productModel";

export interface ProductCategoriesKey {
    categoryId: number;
    productBarcode: string;
}

export interface ProductCategoriesModel {
    id: number;
    categoryId: number;
    productBarcode: string;
    category: CategoryModel | null;
    product: ProductModel | null;
}