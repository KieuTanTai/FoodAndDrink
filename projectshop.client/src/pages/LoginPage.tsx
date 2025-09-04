import React from 'react';
import LoginForm from '../components/LoginForm';

const LoginPage: React.FC = () => {
     return (
          <div className="mx-auto flex max-w-[75rem] flex-col items-center justify-center p-4 lg:px-0">
               <LoginForm />
          </div>
     );
};

export default LoginPage;