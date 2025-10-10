/**
 * Represents the different types of products available in the system
 */
export const EProductType = {
    /**
     * Meat products (beef, pork, chicken, fish, etc.)
     */
    MEAT: 'meat',

    /**
     * Drink products (soft drinks, juices, alcoholic beverages, etc.)
     */
    DRINK: 'drink',

    /**
     * Snack products (chips, cookies, nuts, candy, etc.)
     */
    SNACK: 'snack',

    /**
     * Vegetable products (leafy greens, root vegetables, etc.)
     */
    VEGETABLE: 'vegetable',

    /**
     * Fruit products (fresh fruits, seasonal fruits, etc.)
     */
    FRUIT: 'fruit'
} as const;

export type EProductType = typeof EProductType[keyof typeof EProductType];

/**
 * Array of all product types for iteration or selection lists
 */
export const ProductTypeList = [
    { value: EProductType.MEAT, label: 'Meat' },
    { value: EProductType.DRINK, label: 'Drink' },
    { value: EProductType.SNACK, label: 'Snack' },
    { value: EProductType.VEGETABLE, label: 'Vegetable' },
    { value: EProductType.FRUIT, label: 'Fruit' }
] as const;