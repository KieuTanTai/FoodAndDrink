/**
 * Represents the different types of meat products
 */
export const EMeatType = {
    /**
     * Beef products
     */
    BEEF: 'beef',

    /**
     * Pork products
     */
    PORK: 'pork',

    /**
     * Chicken products
     */
    CHICKEN: 'chicken',

    /**
     * Fish products
     */
    FISH: 'fish',

    /**
     * Lamb products
     */
    LAMB: 'lamb',

    /**
     * Duck products
     */
    DUCK: 'duck',

    /**
     * Shrimp products
     */
    SHRIMP: 'shrimp',

    /**
     * Crab products
     */
    CRAB: 'crab',

    /**
     * Goat products
     */
    GOAT: 'goat',

    /**
     * Turkey products
     */
    TURKEY: 'turkey',

    /**
     * Rabbit products
     */
    RABBIT: 'rabbit',

    /**
     * Shellfish products
     */
    SHELLFISH: 'shellfish',

    /**
     * Seafood products
     */
    SEAFOOD: 'seafood',

    /**
     * Other types of meat
     */
    OTHER: 'other'
} as const;

export type EMeatType = typeof EMeatType[keyof typeof EMeatType];

/**
 * Array of all meat types for iteration or selection lists
 */
export const MeatTypeList = [
    { value: EMeatType.BEEF, label: 'Beef' },
    { value: EMeatType.PORK, label: 'Pork' },
    { value: EMeatType.CHICKEN, label: 'Chicken' },
    { value: EMeatType.FISH, label: 'Fish' },
    { value: EMeatType.LAMB, label: 'Lamb' },
    { value: EMeatType.DUCK, label: 'Duck' },
    { value: EMeatType.SHRIMP, label: 'Shrimp' },
    { value: EMeatType.CRAB, label: 'Crab' },
    { value: EMeatType.GOAT, label: 'Goat' },
    { value: EMeatType.TURKEY, label: 'Turkey' },
    { value: EMeatType.RABBIT, label: 'Rabbit' },
    { value: EMeatType.SHELLFISH, label: 'Shellfish' },
    { value: EMeatType.SEAFOOD, label: 'Seafood' },
    { value: EMeatType.OTHER, label: 'Other' }
] as const;