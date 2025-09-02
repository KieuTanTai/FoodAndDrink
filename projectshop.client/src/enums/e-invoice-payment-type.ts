export const EInvoicePaymentType = {
    COD: 0,
    PREPAID: 1
} as const;

export type EInvoicePaymentType = typeof EInvoicePaymentType[keyof typeof EInvoicePaymentType];