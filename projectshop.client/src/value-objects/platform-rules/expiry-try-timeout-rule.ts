import type { BaseRule } from "./base-rules";

export interface TryTimeoutRule extends BaseRule {
    type: 'try-timeout';
    maxTryTimes: number;
  }