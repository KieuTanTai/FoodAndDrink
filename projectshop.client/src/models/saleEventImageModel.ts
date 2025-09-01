import type { SaleEventModel } from "./saleEventModel";

export interface SaleEventImageModel {
    saleEventImageId: number;
    saleEventId: number;
    saleEventImageUrl: string;
    saleEventImagePriority: number;
    saleEventImageCreatedDate: string;
    saleEventImageLastUpdatedDate: string;
    saleEvent: SaleEventModel | null;
}