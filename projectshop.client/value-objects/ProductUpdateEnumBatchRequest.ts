export interface ProductUpdateEnumBatchRequest<TEnum> {
    barcodes: string[];
    values: TEnum[];
}