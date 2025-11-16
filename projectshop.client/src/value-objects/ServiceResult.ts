import type { JsonLogEntry } from "./json-log-entry";

export interface ServiceResult<TEntity> {
    logEntries: JsonLogEntry[] | null;
    data: TEntity | null;
    isSuccess: boolean;
}