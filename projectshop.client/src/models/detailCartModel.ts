import type{ ProductModel } from './productModel';
import type{ CartModel } from "./cartModel";

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