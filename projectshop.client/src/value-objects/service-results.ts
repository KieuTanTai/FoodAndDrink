import type { JsonLogEntry } from "./json-log-entry";

export interface ServiceResults<TEntity> {
    logEntries: JsonLogEntry[] | null;
    data: TEntity[] | null;
    isSuccess: boolean;
}