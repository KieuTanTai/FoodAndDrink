import type { BasePlatformRule } from "./base-platform-rules";

export interface CookieExpiryRule extends BasePlatformRule {
    type: 'cookie-expiry'; // xác định đúng loại
    maxAgeDays: number;
}
