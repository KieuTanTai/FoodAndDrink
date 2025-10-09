import type{ ProductModel } from './product-model';
import type{ CartModel } from "./cart-model";

export interface DetailCartModel {
    detailCartId: number;
    cartId: number;
    productBarcode: string;
    detailCartAddedDate: string;
    detailCartPrice: number;
    detailCartQuantity: number;
    cart: CartModel | null;
    product: ProductModel | null;
}