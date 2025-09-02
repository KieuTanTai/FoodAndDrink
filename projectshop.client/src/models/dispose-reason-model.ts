import type{ DisposeProductModel } from "./dispose-product-model";

export interface DisposeReasonModel {
    disposeReasonId: number;
    disposeReasonName: string;
    disposeProducts: DisposeProductModel[] | [];
}