import type { BasePlatformRule } from "./base-platform-rules";

export interface FetchTimeoutRule extends BasePlatformRule {
  type: 'fetch-timeout';
  maxFetchTimeout: number;
}