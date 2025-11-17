import type { CategoryModel } from "@/models/CategoryModel";
import type { ProductModel } from "@/models/ProductModel";

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
