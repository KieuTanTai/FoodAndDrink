import type{ CustomerModel } from "./customerModel";
import type{ DetailCartModel } from "./detailCartModel";

export interface CartModel {
    cartId: number;
    customerId: number;
    cartTotalPrice: number;
    customer: CustomerModel | null;
    detailCarts: DetailCartModel[] | [];
}