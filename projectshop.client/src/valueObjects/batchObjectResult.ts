import { BatchItemResult } from "./batchItemResult";

export interface BatchObjectResult<TEntity> {
    validEntities: TEntity[];
    batchResults: BatchItemResult<TEntity>[];
}