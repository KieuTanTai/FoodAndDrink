import type { BaseRule } from "./base-rules";

export default interface MessageTimeoutRule extends BaseRule {
    type: "message-timeout";
    maxMessageTimeout: number;
}