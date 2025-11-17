import { BasePlatformRule } from "./BasePlatformRules";

export interface CookieExpiryRule extends BasePlatformRule {
    type: 'cookie-expiry'; // xác định đúng loại
    maxAgeDays: number;
}
