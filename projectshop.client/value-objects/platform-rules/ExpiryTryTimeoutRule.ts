import { BasePlatformRule } from "./BasePlatformRules";

export interface TryTimeoutRule extends BasePlatformRule {
  type: 'try-timeout';
  maxTryTimes: number;
}