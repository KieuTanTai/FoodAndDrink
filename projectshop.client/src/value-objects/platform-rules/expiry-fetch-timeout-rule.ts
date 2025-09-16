import type { BaseRule } from "./base-rules";

export interface FetchTimeoutRule extends BaseRule {
    type: 'fetch-timeout';
    maxFetchTimeout: number;
  }