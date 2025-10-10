/**
 * Represents the sweetness level of fruit products
 */
export const ESweetness = {
    /**
     * Very low sweetness
     */
    VERY_LOW: 'very_low',

    /**
     * Low sweetness
     */
    LOW: 'low',

    /**
     * Medium sweetness
     */
    MEDIUM: 'medium',

    /**
     * High sweetness
     */
    HIGH: 'high',

    /**
     * Very high sweetness
     */
    VERY_HIGH: 'very_high'
} as const;

export type ESweetness = typeof ESweetness[keyof typeof ESweetness];

/**
 * Array of all sweetness levels for iteration or selection lists
 */
export const SweetnessLevelList = [
    { value: ESweetness.VERY_LOW, label: 'Very Low' },
    { value: ESweetness.LOW, label: 'Low' },
    { value: ESweetness.MEDIUM, label: 'Medium' },
    { value: ESweetness.HIGH, label: 'High' },
    { value: ESweetness.VERY_HIGH, label: 'Very High' }
] as const;