import axios from 'axios';
import type { UILoginData } from '../ui-types/login';
import type { UISignupData } from '../ui-types/signup';
import type { AccountModel } from '../models/account-model';
import type { UIForgotPasswordData } from '../ui-types/forgot-password';
import type { ServiceResult } from '../value-objects/service-result';
import type { AccountNavigationOptions } from '../value-objects/get-navigation-property-options/account-navigation-options';


export async function login(form: UILoginData, navigation: AccountNavigationOptions) : Promise<ServiceResult<AccountModel>> {
     try {
          const response = await axios.post('https://localhost:5294/api/accountservices/login', form, {params: {
               isGetEmployee: navigation.isGetEmployee,
               isGetCustomer: navigation.isGetCustomer,
               isGetRolesOfUsers: navigation.isGetRolesOfUsers,
          }, withCredentials: true});
          return response.data as ServiceResult<AccountModel>;
     } catch (error) {
          throw new Error('Login failed', error as { cause?: Error } | undefined);
     }
}

export async function signup(form: UISignupData) : Promise<ServiceResult<AccountModel>> {
     try {
          const response = await axios.post('/api/accountservices/signup', form);
          return response.data as ServiceResult<AccountModel>;
     } catch (error) {
          throw new Error('Signup failed', error as { cause?: Error } | undefined);
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
          throw new Error('Authentication failed', error as { cause?: Error } | undefined);
     }
}

export async function forgotPassword(form: UIForgotPasswordData) : Promise<ServiceResult<string>> {
     try {
          const response = await axios.post('/api/accountservices/forgot-password', form);
          return response.data as ServiceResult<string>;
     } catch (error) {
          throw new Error('Forgot password request failed', error as { cause?: Error } | undefined);
     }
}