export const EPaymentMethodType = {
    VISA_OR_MASTERCARD: 0,
    BANKING: 1,
    MOMO: 2
} as const;

export type EPaymentMethodType = typeof EPaymentMethodType[keyof typeof EPaymentMethodType];