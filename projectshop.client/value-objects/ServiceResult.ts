import type { JsonLogEntry } from "./JsonLogEntry.ts";

export interface ServiceResult<TEntity> {
    logEntries: JsonLogEntry[] | null;
    data: TEntity | null;
    isSuccess: boolean;
}