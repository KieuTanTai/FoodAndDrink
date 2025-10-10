export const EProductUnit = {
    KG: 'kg',
    GRAM: 'gram',
    LITER: 'liter',
    ML: 'ml',
    LB: 'lb',
    BOX: 'box',
    PACKET: 'packet',
    BOTTLE: 'bottle',
    OZ: 'oz',
    SET: 'set'
} as const;

export type EProductUnit = typeof EProductUnit[keyof typeof EProductUnit];