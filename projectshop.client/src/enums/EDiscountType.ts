export const EDiscountType = {
    PERCENT: 'percent',
    AMOUNT: 'amount'
} as const;

export type EDiscountType = typeof EDiscountType[keyof typeof EDiscountType];