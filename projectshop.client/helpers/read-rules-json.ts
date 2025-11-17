import { BasePlatformRule } from "@/value-objects/platform-rules/BasePlatformRules";
import { CookieExpiryRule } from "@/value-objects/platform-rules/ExpiryCookieRule";
import { FetchTimeoutRule } from "@/value-objects/platform-rules/ExpiryFetchTimeoutRule";
import { TryTimeoutRule } from "@/value-objects/platform-rules/ExpiryTryTimeoutRule";
import MessageTimeoutRule from "@/value-objects/platform-rules/MessageTimeoutRule";
import axios from "axios";

type PlatformRules = {
  "custom-rules": Record<string, BasePlatformRule>;
  "rules-version": string;
};

let rulesCache: PlatformRules | null = null;
let rulesPromise: Promise<PlatformRules> | null = null;

async function getCustomRules(): Promise<PlatformRules> {
  if (rulesCache) return rulesCache;
  if (!rulesPromise) {
    // Use backend API endpoint directly with axios
    const backendApiUrl =
      process.env.NEXT_PUBLIC_BACKEND_API_URL ||
      "https://localhost:5294/api/platform-rules";

    rulesPromise = axios
      .get<PlatformRules>(backendApiUrl, {
        withCredentials: true,
      })
      .then((response) => {
        rulesCache = response.data;
        return response.data;
      })
      .catch((err) => {
        rulesPromise = null;
        throw err;
      });
  }
  return rulesPromise;
}

export function isExpiryCookieRule(
  rule: BasePlatformRule
): rule is CookieExpiryRule {
  return rule.type === "cookie-expiry";
}

export function isTryTimeoutRule(
  rule: BasePlatformRule
): rule is TryTimeoutRule {
  return rule.type === "try-timeout";
}

export function isFetchTimeoutRule(
  rule: BasePlatformRule
): rule is FetchTimeoutRule {
  return rule.type === "fetch-timeout";
}

export function isMessageTimeoutRule(
  rule: BasePlatformRule
): rule is MessageTimeoutRule {
  return rule.type === "message-timeout";
}

export async function readCustomRuleExpiryCookie(): Promise<BasePlatformRule> {
  const customRules = await getCustomRules();
  return customRules["custom-rules"]["expiry-cookie"];
}

export async function readCustomRuleExpiryTryTimeout(): Promise<BasePlatformRule> {
  const customRules = await getCustomRules();
  return customRules["custom-rules"]["expiry-try-timeout"];
}

export async function readCustomRuleExpiryFetchTimeout(): Promise<BasePlatformRule> {
  const customRules = await getCustomRules();
  return customRules["custom-rules"]["expiry-fetch-timeout"];
}

export async function readCustomRuleMessageTimeout(): Promise<BasePlatformRule> {
  const customRules = await getCustomRules();
  return customRules["custom-rules"]["message-timeout"];
}
