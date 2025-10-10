export const EPaymentMethodType = {
    VISA_OR_MASTERCARD: 'visa_or_mastercard',
    BANKING: 'banking',
    MOMO: 'momo'
} as const;

export type EPaymentMethodType = typeof EPaymentMethodType[keyof typeof EPaymentMethodType];