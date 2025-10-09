export const EInventoryMovementReason = {
    WAREHOUSE_TO_STORE: 0,
    STORE_TO_WAREHOUSE: 1,
    WAREHOUSE_TO_WAREHOUSE: 2,
    STORE_TO_STORE: 3,
    SUPPLIER_TO_STORE: 4,
    OTHER: 5
} as const;

export type EInventoryMovementReason = typeof EInventoryMovementReason[keyof typeof EInventoryMovementReason];