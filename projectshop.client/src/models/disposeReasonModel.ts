import { DisposeProductModel } from "./disposeProductModel";

export interface DisposeReasonModel {
    disposeReasonId: number;
    disposeReasonName: string;
    disposeProducts: DisposeProductModel[] | [];
}