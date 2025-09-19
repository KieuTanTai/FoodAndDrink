// import Cookies from 'js-cookie';
import type { UILoginData } from '../ui-types/login';
import type { UISignupData } from '../ui-types/signup';
import type { AccountModel } from '../models/account-model';
import type { UIForgotPasswordData } from '../ui-types/forgot-password';
import type { ServiceResult } from '../value-objects/service-result';
import type { AccountNavigationOptions } from '../value-objects/get-navigation-property-options/account-navigation-options';
import { InvalidValueError } from '../value-objects/custom-error/invalidValueError';
import type { JsonLogEntry } from '../value-objects/json-log-entry';
import axios, { isAxiosError, type AxiosResponse } from 'axios';

export async function login(form: UILoginData, navigation: AccountNavigationOptions) : Promise<ServiceResult<AccountModel>> {
     try {
          const response = await axios.post('https://localhost:5294/api/accountservices/login', form, {params: {
               isGetEmployee: navigation.isGetEmployee,
               isGetCustomer: navigation.isGetCustomer,
               isGetRolesOfUsers: navigation.isGetRolesOfUsers,
          }, withCredentials: form.rememberMe});
          const result = response.data as ServiceResult<AccountModel>;
          console.log(result);
          // setCookie(result);
          return result;
     } catch (error) {
          if (error instanceof InvalidValueError)
               console.error(error.message);
          throw new Error('Login failed', error as { cause?: Error } | undefined);
     }
}

export async function signup(form: UISignupData): Promise<ServiceResult<AccountModel> | JsonLogEntry[]> {
     try {
          const sendData = { Email: form.email, Password: form.password };
          const response: AxiosResponse< ServiceResult<AccountModel>> = await axios.post(
               'https://localhost:5294/api/accountservices/signup',
               sendData
          );
          console.log('[signup] Kết quả trả về:', response.data);
          return response.data;
     } catch (error: unknown) {
          if (isAxiosError(error)) {
               console.error('[signup] Lỗi từ Axios:', error.message, error.code);
               const responseData = error.response?.data;
               console.error('[signup] Dữ liệu lỗi từ server:', responseData);
               const logEntries: JsonLogEntry[] = responseData?.logEntries ?? [];
               if (logEntries.length > 0) {
                    console.table(logEntries);
                    return logEntries;
               }
               return responseData;
          } else {
               console.error('[signup] Lỗi không phải AxiosError:', error);
               throw error;
          }
     }
 }

export async function getCurrentAccount(navigation: AccountNavigationOptions) : Promise<ServiceResult<AccountModel>> {
     try {
          const response = await axios.get('https://localhost:5294/api/accountservices/me', {params: {
               isGetEmployee: navigation.isGetEmployee,
               isGetCustomer: navigation.isGetCustomer,
               isGetRolesOfUsers: navigation.isGetRolesOfUsers,
          }, withCredentials: true});
          return response.data as ServiceResult<AccountModel>;
     } catch (error) {
          if (error instanceof Error)
               console.error(error.message);
          return Promise.reject();
     }
}

export async function forgotPassword(form: UIForgotPasswordData) : Promise<ServiceResult<string>> {
     try {
          const response = await axios.post('https://localhost:5294/api/accountservices/forgot-password', form);
          return response.data as ServiceResult<string>;
     } catch (error) {
          throw new Error('Forgot password request failed', error as { cause?: Error } | undefined);
     }
}

export async function logout() {
     try {
          const response = await axios.delete('https://localhost:5294/api/accountservices/logout', {withCredentials: true});
          return response.data as string;
     } catch (error) {
          throw new Error('Error when fetch to logout api', error as { cause?: Error } | undefined);
     }
}

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