/**
 * Represents the storage method for products
 */
export const EStorageMethod = {
    /**
     * Requires refrigeration
     */
    REFRIGERATED: 'refrigerated',

    /**
     * Frozen storage
     */
    FROZEN: 'frozen',

    /**
     * Room temperature storage
     */
    ROOM_TEMPERATURE: 'room_temperature',

    /**
     * Dry storage conditions
     */
    DRY_STORAGE: 'dry_storage'
} as const;

export type EStorageMethod = typeof EStorageMethod[keyof typeof EStorageMethod];

/**
 * Array of all storage methods for iteration or selection lists
 */
export const StorageMethodList = [
    { value: EStorageMethod.REFRIGERATED, label: 'Refrigerated' },
    { value: EStorageMethod.FROZEN, label: 'Frozen' },
    { value: EStorageMethod.ROOM_TEMPERATURE, label: 'Room Temperature' },
    { value: EStorageMethod.DRY_STORAGE, label: 'Dry Storage' }
] as const;