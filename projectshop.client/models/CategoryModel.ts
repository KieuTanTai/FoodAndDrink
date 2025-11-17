import type { ProductCategoriesModel } from "@/models/ProductCategoriesModel";
export interface CategoryModel {
  categoryId: number;
  categoryName: string;
  categoryStatus: boolean;
  productCategories: ProductCategoriesModel[] | [];
}
