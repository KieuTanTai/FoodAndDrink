import type { DisposeProductModel } from "@/models/DisposeProductModel";

export interface DisposeReasonModel {
  disposeReasonId: number;
  disposeReasonName: string;
  disposeProducts: DisposeProductModel[] | [];
}
