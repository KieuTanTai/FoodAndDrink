export const ECompareType = {
    EQUAL: 0,
    LESS_THAN: 1,
    GREATER_THAN: 2,
    GREATER_THAN_OR_EQUAL: 3,
    LESS_THAN_OR_EQUAL: 4
} as const;

export type ECompareType = typeof ECompareType[keyof typeof ECompareType];