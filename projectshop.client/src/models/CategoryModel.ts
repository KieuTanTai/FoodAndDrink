import type{ ProductCategoriesModel } from './product-categories-model';
export interface CategoryModel {
    categoryId: number;
    categoryName: string;
    categoryStatus: boolean;
    productCategories: ProductCategoriesModel[] | [];
}