/**
 * Represents the different types of vegetables
 */
export const EVegetableType = {
    /**
     * Leafy vegetables (lettuce, spinach, kale, etc.)
     */
    LEAFY: 'leafy',

    /**
     * Root vegetables (carrots, radishes, etc.)
     */
    ROOT: 'root',

    /**
     * Tuber vegetables (potatoes, sweet potatoes, etc.)
     */
    TUBER: 'tuber',

    /**
     * Fruit vegetables (tomatoes, peppers, eggplants, etc.)
     */
    FRUIT_VEGETABLE: 'fruit_vegetable',

    /**
     * Bulb vegetables (onions, garlic, shallots, etc.)
     */
    BULB: 'bulb',

    /**
     * Stem vegetables (celery, asparagus, bamboo shoots, etc.)
     */
    STEM: 'stem',

    /**
     * Flower vegetables (broccoli, cauliflower, artichokes, etc.)
     */
    FLOWER: 'flower',

    /**
     * Sprout vegetables (bean sprouts, alfalfa sprouts, etc.)
     */
    SPROUT: 'sprout',

    /**
     * Fungi (mushrooms, etc.)
     */
    FUNGI: 'fungi',

    /**
     * Other types of vegetables
     */
    OTHER: 'other'
} as const;

export type EVegetableType = typeof EVegetableType[keyof typeof EVegetableType];

/**
 * Array of all vegetable types for iteration or selection lists
 */
export const VegetableTypeList = [
    { value: EVegetableType.LEAFY, label: 'Leafy' },
    { value: EVegetableType.ROOT, label: 'Root' },
    { value: EVegetableType.TUBER, label: 'Tuber' },
    { value: EVegetableType.FRUIT_VEGETABLE, label: 'Fruit Vegetable' },
    { value: EVegetableType.BULB, label: 'Bulb' },
    { value: EVegetableType.STEM, label: 'Stem' },
    { value: EVegetableType.FLOWER, label: 'Flower' },
    { value: EVegetableType.SPROUT, label: 'Sprout' },
    { value: EVegetableType.FUNGI, label: 'Fungi' },
    { value: EVegetableType.OTHER, label: 'Other' }
] as const;