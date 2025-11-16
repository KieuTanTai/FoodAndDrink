import type { BasePlatformRule } from "./base-platform-rules";

export interface TryTimeoutRule extends BasePlatformRule {
  type: 'try-timeout';
  maxTryTimes: number;
}