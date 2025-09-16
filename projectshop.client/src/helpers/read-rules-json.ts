import customRules from "../../../PlatformRules/platform-rules.json"
import type { BaseRule } from "../value-objects/platform-rules/base-rules";
import type { CookieExpiryRule } from "../value-objects/platform-rules/expiry-cookie-rule";
import type { FetchTimeoutRule } from "../value-objects/platform-rules/expiry-fetch-timeout-rule";
import type { TryTimeoutRule } from "../value-objects/platform-rules/expiry-try-timeout-rule";
import type MessageTimeoutRule from "../value-objects/platform-rules/message-timeout-rule";

export function isExpiryCookieRule(rule: BaseRule): rule is CookieExpiryRule {
    return rule.type === "cookie-expiry";
}

export function isTryTimeoutRule(rule: BaseRule): rule is TryTimeoutRule {
    return rule.type === "try-timeout";
}

export function isFetchTimeoutRule(rule: BaseRule): rule is FetchTimeoutRule { 
    return rule.type === "fetch-timeout";
}

export function isMessageTimeoutRule(rule: BaseRule): rule is MessageTimeoutRule {
    return rule.type === "message-timeout";
}

export async function readCustomRuleExpiryCookie() : Promise<BaseRule> {
    return customRules["custom-rules"]["expiry-cookie"];
} 

export async function readCustomRuleExpiryTryTimeout() : Promise<BaseRule> {
    return customRules["custom-rules"]["expiry-try-timeout"];
}

export async function readCustomRuleExpiryFetchTimeout() : Promise<BaseRule> {
    return customRules["custom-rules"]["expiry-fetch-timeout"];
}

export async function readCustomRuleMessageTimeout(): Promise<BaseRule> {
    return customRules["custom-rules"]["message-timeout"];
}