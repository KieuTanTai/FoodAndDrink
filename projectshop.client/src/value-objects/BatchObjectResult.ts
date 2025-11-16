import type { BatchItemResult } from "./batch-item-result";

export interface BatchObjectResult<TEntity> {
    validEntities: TEntity[];
    batchResults: BatchItemResult<TEntity>[];
}