import type { BasePlatformRule } from "./base-platform-rules";

export default interface MessageTimeoutRule extends BasePlatformRule {
    type: "message-timeout";
    maxMessageTimeout: number;
}