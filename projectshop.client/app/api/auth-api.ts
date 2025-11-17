
import { AccountModel } from "@/models/AccountModel";
import { UILoginData } from "@/props/ui-props/accounts/Login";
import { UISignupData } from "@/props/ui-props/accounts/Signup";
import { InvalidValueError } from "@/value-objects/custom-error/invalidValueError";
import { JsonLogEntry } from "@/value-objects/JsonLogEntry";
import axios, { isAxiosError, type AxiosResponse } from "axios";

export async function login(form: UILoginData): Promise<AccountModel> {
  try {
    const response = await axios.post(
      "https://localhost:5294/api/account/login",
      {
        Email: form.email,
        Password: form.password,
        RememberMe: form.rememberMe,
      },
      {
        withCredentials: true, // PHẢI Ở ĐÂY - trong config, không phải trong body!
      }
    );
    const result = response.data as AccountModel;
    console.log(result);
    return result;
  } catch (error) {
    if (error instanceof InvalidValueError) console.error(error.message);
    throw new Error("Login failed", error as { cause?: Error } | undefined);
  }
}

export async function signup(
  form: UISignupData
): Promise<AccountModel | JsonLogEntry[]> {
  try {
    const sendData = { Email: form.email, Password: form.password };
    const response: AxiosResponse<AccountModel> = await axios.post(
      "https://localhost:5294/api/account/register",
      sendData
    );
    console.log("[signup] Kết quả trả về:", response.data);
    return response.data;
  } catch (error: unknown) {
    if (isAxiosError(error)) {
      console.error("[signup] Lỗi từ Axios:", error.message, error.code);
      const responseData = error.response?.data;
      console.error("[signup] Dữ liệu lỗi từ server:", responseData);
      const logEntries: JsonLogEntry[] = responseData?.logEntries ?? [];
      if (logEntries.length > 0) {
        console.table(logEntries);
        return logEntries;
      }
      return responseData;
    } else {
      console.error("[signup] Lỗi không phải AxiosError:", error);
      throw error;
    }
  }
}

export async function checkExistedByEmail(email: string): Promise<boolean> {
  try {
    const response = await axios.get(
      "https://localhost:5294/api/account/by-username",
      { params: { userName: email } }
    );
    if (response && response.status === 200) return true;
    return false;
  } catch (error) {
    if (error instanceof Error) console.error(error.message);
    return false;
  }
}

export async function logout() {
  try {
    const response = await axios.delete(
      "https://localhost:5294/api/account/logout",
      { withCredentials: true }
    );
    return response.data as string;
  } catch (error) {
    throw new Error(
      "Error when fetch to logout api",
      error as { cause?: Error } | undefined
    );
  }
}

export async function forgotPassword() {}

// async function setCookie(account: ServiceResult<AccountModel>) {
//      if (account)
//      {
//           let expiry: number = 0;
//           const rule: BaseRule = await readCustomRuleExpiryCookie();
//           if (isExpiryCookieRule(rule))
//                expiry = rule.maxAgeDays;
//           else
//                throw new InvalidValueError("can't get value of expiry cookie");
//           Cookies.set("session_login", account.token, {expires: expiry});
//      }
// }
