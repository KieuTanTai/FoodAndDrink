// PlatformRules type for platform-rules.json structure
export interface PlatformRules {
  "custom-rules": {
    [key: string]: {
      type: string;
      enabled: boolean;
      // Optional fields for known rules
      maxAgeDays?: number;
      maxTryTimes?: number;
      maxFetchTimeout?: number;
      maxMessageTimeout?: number;
      maxCacheExpiryMinutes?: number;
      maxRecords?: number;
      defaultPageSize?: number;
      maxRequestsPerMinute?: number;
    };
  };
  "rules-version": string;
}
