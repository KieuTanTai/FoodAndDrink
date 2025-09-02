import axios from 'axios';
import type { LoginData } from '../ui-types/login';
import type { SignupData } from '../ui-types/signup';
import type { AccountModel } from '../models/account-model';
import type { ForgotPasswordData } from '../ui-types/forgot-password';


export async function login(form: LoginData) : Promise<AccountModel> {
     try {
          const response = await axios.post('/api/accountservices/login', form);
          return response.data as AccountModel;
     } catch (error) {
          throw new Error('Login failed');
     }
}

export async function signup(form: SignupData) : Promise<AccountModel> {
     try {
          const response = await axios.post('/api/accountservices/signup', form);
          return response.data as AccountModel;
     } catch (error) {
          throw new Error('Signup failed');
     }
}

export async function forgotPassword(form: ForgotPasswordData) : Promise<string> {
     try {
          const response = await axios.post('/api/accountservices/forgot-password', form);
          return response.data as string;
     } catch (error) {
          throw new Error('Forgot password request failed');
     }
}