import { ProductCategoriesModel } from './productCategoriesModel';
export interface CategoryModel {
    categoryId: number;
    categoryName: string;
    categoryStatus: boolean;
    productCategories: ProductCategoriesModel[] | [];
}