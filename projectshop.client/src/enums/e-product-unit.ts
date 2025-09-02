export const EProductUnit = {
    KG: 0,
    GRAM: 1,
    LITER: 2,
    ML: 3,
    LB: 4,
    BOX: 5,
    PACKET: 6,
    BOTTLE: 7,
    OZ: 8,
    SET: 9
} as const;

export type EProductUnit = typeof EProductUnit[keyof typeof EProductUnit];