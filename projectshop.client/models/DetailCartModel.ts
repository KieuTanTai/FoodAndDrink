import type { ProductModel } from "@/models/ProductModel";
import type { CartModel } from "@/models/CartModel";

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
