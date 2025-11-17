import { BasePlatformRule } from "./BasePlatformRules";

export interface FetchTimeoutRule extends BasePlatformRule {
  type: 'fetch-timeout';
  maxFetchTimeout: number;
}