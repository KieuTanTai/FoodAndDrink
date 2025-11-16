/**
 * Represents the different types of beverages
 */
export const EBeverageType = {
    /**
     * Soft drinks (carbonated and non-carbonated)
     */
    SOFT_DRINK: 'soft_drink',

    /**
     * Fruit and vegetable juices
     */
    JUICE: 'juice',

    /**
     * Tea beverages
     */
    TEA: 'tea',

    /**
     * Coffee beverages
     */
    COFFEE: 'coffee',

    /**
     * Energy drinks
     */
    ENERGY_DRINK: 'energy_drink',

    /**
     * Sports drinks
     */
    SPORTS_DRINK: 'sports_drink',

    /**
     * Kombucha beverages
     */
    KOMBUCHA: 'kombucha',

    /**
     * Infused water
     */
    INFUSED_WATER: 'infused_water',

    /**
     * Protein shake beverages
     */
    PROTEIN_SHAKE: 'protein_shake',

    /**
     * Alcoholic beverages
     */
    ALCOHOLIC: 'alcoholic',

    /**
     * Wine beverages
     */
    WINE: 'wine',

    /**
     * Beer beverages
     */
    BEER: 'beer',

    /**
     * Water (still and sparkling)
     */
    WATER: 'water',

    /**
     * Milk beverages
     */
    MILK: 'milk',

    /**
     * Plant-based milk alternatives
     */
    PLANT_MILK: 'plant_milk',

    /**
     * Yogurt drinks
     */
    YOGURT_DRINK: 'yogurt_drink',

    /**
     * Smoothie beverages
     */
    SMOOTHIE: 'smoothie',

    /**
     * Syrup beverages
     */
    SYRUP: 'syrup',

    /**
     * Other types of beverages
     */
    OTHER: 'other'
} as const;

export type EBeverageType = typeof EBeverageType[keyof typeof EBeverageType];

/**
 * Array of all beverage types for iteration or selection lists
 */
export const BeverageTypeList = [
    { value: EBeverageType.SOFT_DRINK, label: 'Soft Drink' },
    { value: EBeverageType.JUICE, label: 'Juice' },
    { value: EBeverageType.TEA, label: 'Tea' },
    { value: EBeverageType.COFFEE, label: 'Coffee' },
    { value: EBeverageType.ENERGY_DRINK, label: 'Energy Drink' },
    { value: EBeverageType.SPORTS_DRINK, label: 'Sports Drink' },
    { value: EBeverageType.KOMBUCHA, label: 'Kombucha' },
    { value: EBeverageType.INFUSED_WATER, label: 'Infused Water' },
    { value: EBeverageType.PROTEIN_SHAKE, label: 'Protein Shake' },
    { value: EBeverageType.ALCOHOLIC, label: 'Alcoholic' },
    { value: EBeverageType.WINE, label: 'Wine' },
    { value: EBeverageType.BEER, label: 'Beer' },
    { value: EBeverageType.WATER, label: 'Water' },
    { value: EBeverageType.MILK, label: 'Milk' },
    { value: EBeverageType.PLANT_MILK, label: 'Plant Milk' },
    { value: EBeverageType.YOGURT_DRINK, label: 'Yogurt Drink' },
    { value: EBeverageType.SMOOTHIE, label: 'Smoothie' },
    { value: EBeverageType.SYRUP, label: 'Syrup' },
    { value: EBeverageType.OTHER, label: 'Other' }
] as const;