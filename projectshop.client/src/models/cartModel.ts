import { CustomerModel } from "./customerModel";
import { DetailCartModel } from "./detailCartModel";

export interface CartModel {
    cartId: number;
    customerId: number;
    cartTotalPrice: number;
    customer: CustomerModel | null;
    detailCarts: DetailCartModel[] | [];
}