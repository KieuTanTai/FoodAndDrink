import type{ CustomerModel } from "./customer-model";
import type{ DetailCartModel } from "./detail-cart-model";

export interface CartModel {
    cartId: number;
    customerId: number;
    cartTotalPrice: number;
    customer: CustomerModel | null;
    detailCarts: DetailCartModel[] | [];
}