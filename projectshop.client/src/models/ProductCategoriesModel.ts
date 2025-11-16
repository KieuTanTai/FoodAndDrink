import type { CategoryModel } from "./category-model";
import type { ProductModel } from "./product-model";

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