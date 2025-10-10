export const EQueryTimeType = {
    MONTH_AND_YEAR: 'month_and_year',
    YEAR: 'year',
    DATE_TIME_RANGE: 'date_time_range',
    DATE_TIME: 'date_time'
} as const;

export type EQueryTimeType = typeof EQueryTimeType[keyof typeof EQueryTimeType];