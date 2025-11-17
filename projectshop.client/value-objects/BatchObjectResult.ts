import type { BatchItemResult } from "./BatchItemResult.ts";

export interface BatchObjectResult<TEntity> {
    validEntities: TEntity[];
    batchResults: BatchItemResult<TEntity>[];
}