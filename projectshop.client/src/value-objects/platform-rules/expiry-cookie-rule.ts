import type { BaseRule } from "./base-rules";

export interface CookieExpiryRule extends BaseRule {
    type: 'cookie-expiry'; // xác định đúng loại
    maxAgeDays: number;
}
  