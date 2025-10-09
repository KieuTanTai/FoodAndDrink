export const EQueryTimeType = {
    MONTH_AND_YEAR: 0,
    YEAR: 1,
    DATE_TIME_RANGE: 2,
    DATE_TIME: 3
} as const;

export type EQueryTimeType = typeof EQueryTimeType[keyof typeof EQueryTimeType];