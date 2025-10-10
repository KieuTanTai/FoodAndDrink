export const EInvoicePaymentType = {
    COD: 'cod',
    PREPAID: 'prepaid'
} as const;

export type EInvoicePaymentType = typeof EInvoicePaymentType[keyof typeof EInvoicePaymentType];