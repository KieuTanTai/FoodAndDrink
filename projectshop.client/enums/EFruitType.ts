/**
 * Represents the different types of fruit products
 */
export const EFruitType = {
    /**
     * Fresh fruit products
     */
    FRESH: 'fresh',

    /**
     * Dried fruit products
     */
    DRIED_FRUIT: 'dried_fruit',

    /**
     * Canned fruit products
     */
    CANNED_FRUIT: 'canned_fruit',

    /**
     * Other types of fruit products
     */
    OTHER: 'other'
} as const;

export type EFruitType = typeof EFruitType[keyof typeof EFruitType];

/**
 * Array of all fruit types for iteration or selection lists
 */
export const FruitTypeList = [
    { value: EFruitType.FRESH, label: 'Fresh' },
    { value: EFruitType.DRIED_FRUIT, label: 'Dried Fruit' },
    { value: EFruitType.CANNED_FRUIT, label: 'Canned Fruit' },
    { value: EFruitType.OTHER, label: 'Other' }
] as const;