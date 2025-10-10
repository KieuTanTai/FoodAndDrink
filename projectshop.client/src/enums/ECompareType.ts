export const ECompareType = {
    EQUAL: 'equal',
    LESS_THAN: 'less_than',
    GREATER_THAN: 'greater_than',
    GREATER_THAN_OR_EQUAL: 'greater_than_or_equal',
    LESS_THAN_OR_EQUAL: 'less_than_or_equal'
} as const;

export type ECompareType = typeof ECompareType[keyof typeof ECompareType];