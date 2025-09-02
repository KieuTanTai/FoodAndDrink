export interface LoginFormProps {
     userName?: string;
     password?: string;
     rememberMe?: boolean;
     isLoading: boolean;
     errorMessage?: string;
     onUsernameChange: (username: string) => void;
     onPasswordChange: (password: string) => void;
     onRememberMeChange: (rememberMe: boolean) => void;
     onSubmit: (username: string, password: string, rememberMe: boolean) => void;
     onLoginSuccess: (token: string) => void;
     onLoginFailure: (error: string) => void;
}