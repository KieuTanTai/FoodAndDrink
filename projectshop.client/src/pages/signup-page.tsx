import React from 'react';
import SignupForm from '../components/signup-form';

const SignupPage: React.FC = () => {
     return (
          <div className="mx-auto flex max-w-[75rem] flex-col items-center justify-center p-4 lg:px-0">
               <SignupForm />
          </div>
     );
};

export default SignupPage;