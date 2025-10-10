/**
 * Represents the different seasons for harvesting or availability
 */
export const ESeason = {
    /**
     * Spring season
     */
    SPRING: 'spring',

    /**
     * Summer season
     */
    SUMMER: 'summer',

    /**
     * Autumn/Fall season
     */
    AUTUMN: 'autumn',

    /**
     * Winter season
     */
    WINTER: 'winter',

    /**
     * Available year-round
     */
    YEAR_ROUND: 'year_round'
} as const;

export type ESeason = typeof ESeason[keyof typeof ESeason];

/**
 * Array of all seasons for iteration or selection lists
 */
export const SeasonList = [
    { value: ESeason.SPRING, label: 'Spring' },
    { value: ESeason.SUMMER, label: 'Summer' },
    { value: ESeason.AUTUMN, label: 'Autumn' },
    { value: ESeason.WINTER, label: 'Winter' },
    { value: ESeason.YEAR_ROUND, label: 'Year Round' }
] as const;