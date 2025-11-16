export const EInventoryMovementReason = {
    WAREHOUSE_TO_STORE: 'warehouse_to_store',
    STORE_TO_WAREHOUSE: 'store_to_warehouse',
    WAREHOUSE_TO_WAREHOUSE: 'warehouse_to_warehouse',
    STORE_TO_STORE: 'store_to_store',
    SUPPLIER_TO_STORE: 'supplier_to_store',
    OTHER: 'other'
} as const;

export type EInventoryMovementReason = typeof EInventoryMovementReason[keyof typeof EInventoryMovementReason];