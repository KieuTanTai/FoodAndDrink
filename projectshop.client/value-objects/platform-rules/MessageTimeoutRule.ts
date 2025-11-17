import { BasePlatformRule } from "./BasePlatformRules";

export default interface MessageTimeoutRule extends BasePlatformRule {
    type: "message-timeout";
    maxMessageTimeout: number;
}