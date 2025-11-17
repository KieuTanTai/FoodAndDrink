import type { JsonLogEntry } from "./JsonLogEntry.ts";

export interface ServiceResults<TEntity> {
    logEntries: JsonLogEntry[] | null;
    data: TEntity[] | null;
    isSuccess: boolean;
}