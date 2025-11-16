/**
 * Represents the grade quality of meat products
 */
export const EMeatGrade = {
    /**
     * Premium quality meat
     */
    PREMIUM: 'premium',

    /**
     * Standard quality meat
     */
    STANDARD: 'standard',

    /**
     * Economy quality meat
     */
    ECONOMY: 'economy'
} as const;

export type EMeatGrade = typeof EMeatGrade[keyof typeof EMeatGrade];

/**
 * Array of all meat grades for iteration or selection lists
 */
export const MeatGradeList = [
    { value: EMeatGrade.PREMIUM, label: 'Premium' },
    { value: EMeatGrade.STANDARD, label: 'Standard' },
    { value: EMeatGrade.ECONOMY, label: 'Economy' }
] as const;