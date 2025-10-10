/**
 * Represents the different types of snack products
 */
export const ESnackType = {
    /**
     * Chips and crisps
     */
    CHIPS: 'chips',

    /**
     * Cookies and biscuits
     */
    COOKIES: 'cookies',

    /**
     * Nuts and seeds
     */
    NUTS: 'nuts',

    /**
     * Candy and sweets
     */
    CANDY: 'candy',

    /**
     * Chocolate products
     */
    CHOCOLATE: 'chocolate',

    /**
     * Crackers
     */
    CRACKERS: 'crackers',

    /**
     * Ice cream products
     */
    ICE_CREAM: 'ice_cream',

    /**
     * Cheese products
     */
    CHEESE: 'cheese',

    /**
     * Butter products
     */
    BUTTER: 'butter',

    /**
     * Yogurt products
     */
    YOGURT: 'yogurt',

    /**
     * Noodles products
     */
    NOODLES: 'noodles',

    /**
     * Porridge products
     */
    PORRIDGE: 'porridge',

    /**
     * Popcorn products
     */
    POPCORN: 'popcorn',

    /**
     * Dried fruit products
     */
    DRIED_FRUIT: 'dried_fruit',

    /**
     * Seaweed products
     */
    SEAWEED: 'seaweed',

    /**
     * Rice crackers
     */
    RICE_CRACKERS: 'rice_crackers',

    /**
     * Granola bar products
     */
    GRANOLA_BAR: 'granola_bar',

    /**
     * Pudding products
     */
    PUDDING: 'pudding',

    /**
     * Jelly products
     */
    JELLY: 'jelly',

    /**
     * Waffle products
     */
    WAFFLE: 'waffle',

    /**
     * Other types of snacks
     */
    OTHER: 'other'
} as const;

export type ESnackType = typeof ESnackType[keyof typeof ESnackType];

/**
 * Array of all snack types for iteration or selection lists
 */
export const SnackTypeList = [
    { value: ESnackType.CHIPS, label: 'Chips' },
    { value: ESnackType.COOKIES, label: 'Cookies' },
    { value: ESnackType.NUTS, label: 'Nuts' },
    { value: ESnackType.CANDY, label: 'Candy' },
    { value: ESnackType.CHOCOLATE, label: 'Chocolate' },
    { value: ESnackType.CRACKERS, label: 'Crackers' },
    { value: ESnackType.ICE_CREAM, label: 'Ice Cream' },
    { value: ESnackType.CHEESE, label: 'Cheese' },
    { value: ESnackType.BUTTER, label: 'Butter' },
    { value: ESnackType.YOGURT, label: 'Yogurt' },
    { value: ESnackType.NOODLES, label: 'Noodles' },
    { value: ESnackType.PORRIDGE, label: 'Porridge' },
    { value: ESnackType.POPCORN, label: 'Popcorn' },
    { value: ESnackType.DRIED_FRUIT, label: 'Dried Fruit' },
    { value: ESnackType.SEAWEED, label: 'Seaweed' },
    { value: ESnackType.RICE_CRACKERS, label: 'Rice Crackers' },
    { value: ESnackType.GRANOLA_BAR, label: 'Granola Bar' },
    { value: ESnackType.PUDDING, label: 'Pudding' },
    { value: ESnackType.JELLY, label: 'Jelly' },
    { value: ESnackType.WAFFLE, label: 'Waffle' },
    { value: ESnackType.OTHER, label: 'Other' }
] as const;