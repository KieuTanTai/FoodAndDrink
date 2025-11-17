import type { CustomerModel } from "@/models/CustomerModel";
import type { DetailCartModel } from "@/models/DetailCartModel";

export interface CartModel {
  cartId: number;
  customerId: number;
  cartTotalPrice: number;
  customer: CustomerModel | null;
  detailCarts: DetailCartModel[] | [];
}
