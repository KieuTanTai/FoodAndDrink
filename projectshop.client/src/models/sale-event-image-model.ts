import type { SaleEventModel } from "./sale-event-model";

export interface SaleEventImageModel {
    saleEventImageId: number;
    saleEventId: number;
    saleEventImageUrl: string;
    saleEventImagePriority: number;
    saleEventImageCreatedDate: string;
    saleEventImageLastUpdatedDate: string;
    saleEvent: SaleEventModel | null;
}