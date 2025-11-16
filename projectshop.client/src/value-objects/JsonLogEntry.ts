export interface JsonLogEntry {
    queryTime: string;
    queryTimeString: string;
    entityCall: any | null;
    methodCall: any | null;
    entity: any | null;
    message: string | null;
    errorName: string | null;
    errorMessage: string | null;
    affectedRows: number | null;
}