import type { JsonLogEntry } from "./jsonLogEntry";

export interface ServiceResult<TEntity> {
    logEntries: JsonLogEntry[] | null;
    data: TEntity | null;
}