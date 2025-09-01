import { JsonLogEntry } from "./jsonLogEntry";

export interface ServiceResults<TEntity> {
    logEntries: JsonLogEntry[] | null;
    data: TEntity[] | null;
}