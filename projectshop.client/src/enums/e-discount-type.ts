export const EDiscountType = {
    PERCENT: 0,
    AMOUNT: 1
}

export type EDiscountType = typeof EDiscountType[keyof typeof EDiscountType];