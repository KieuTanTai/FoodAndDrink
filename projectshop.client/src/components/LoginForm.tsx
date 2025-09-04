import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserCircle, faLock } from '@fortawesome/free-solid-svg-icons';
import { faGoogle as faGoogleBrand, faFacebook as faFacebookBrand } from '@fortawesome/free-brands-svg-icons';

const LoginForm: React.FC = () => {
     // State to manage form inputs
     const [email, setEmail] = useState('');
     const [password, setPassword] = useState('');
     const [rememberMe, setRememberMe] = useState(false);

     // Handler for form submission
     const handleSubmit = (e: React.FormEvent) => {
          e.preventDefault();
          // Logic to handle user login
          console.log('Email:', email);
          console.log('Mật khẩu:', password);
          console.log('Ghi nhớ tôi:', rememberMe);
          // In a real app, you would send this data to an API
          // to authenticate the user.
     };

     return (
          <div className="w-full max-w-lg space-y-6 rounded-xl border border-gray-200 bg-white p-8 shadow-lg" id="login-form-container">
               <div className="text-center">
                    <h2 className="text-2xl font-bold text-gray-900">Chào mừng đến với cửa hàng</h2>
                    <p className="mt-2 text-sm text-gray-500">Đăng nhập tài khoản để tiếp tục</p>
               </div>
               <form className="space-y-4" onSubmit={handleSubmit} id="login-form">
                    {/* Username input */}
                    <div>
                         <label htmlFor="login-email" className="sr-only">Username</label>
                         <div className="relative">
                              <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                                   <FontAwesomeIcon icon={faUserCircle} className="text-gray-400" />
                              </div>
                              <input
                                   id="login-email"
                                   name="login_email"
                                   type="email"
                                   autoComplete="email"
                                   required
                                   placeholder="Email"
                                   value={email}
                                   onChange={(e) => setEmail(e.target.value)}
                                   className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none"
                              />
                         </div>
                         <div className="mt-1 h-4 text-xs text-red-500" id="login-email-error-msg"></div>
                    </div>
                    {/* Password input */}
                    <div>
                         <label htmlFor="login-password" className="sr-only">Mật khẩu</label>
                         <div className="relative">
                              <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                                   <FontAwesomeIcon icon={faLock} className="text-gray-400" />
                              </div>
                              <input
                                   id="login-password"
                                   name="login_password"
                                   type="password"
                                   autoComplete="current-password"
                                   required
                                   placeholder="Mật khẩu"
                                   value={password}
                                   onChange={(e) => setPassword(e.target.value)}
                                   className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none"
                              />
                         </div>
                         <div className="mt-1 h-4 text-xs text-red-500" id="login-pass-error-msg"></div>
                    </div>
                    {/* Remember me & Forgot password */}
                    <div className="flex items-center justify-between">
                         <div className="flex items-center">
                              <input
                                   id="login-remember-me"
                                   name="remember-me"
                                   type="checkbox"
                                   checked={rememberMe}
                                   onChange={(e) => setRememberMe(e.target.checked)}
                                   className="h-4 w-4 rounded border-gray-300 text-blue-600"
                              />
                              <label htmlFor="login-remember-me" className="ml-2 block text-sm text-gray-900">
                                   Ghi nhớ tôi
                              </label>
                         </div>
                         <div className="text-sm">
                              <a href="#" className="font-medium text-blue-600 hover:text-blue-500">
                                   Quên mật khẩu?
                              </a>
                         </div>
                    </div>
                    {/* Login button */}
                    <div>
                         <button
                              type="submit"
                              className="flex w-full justify-center rounded-lg border border-transparent px-4 py-2 text-sm font-medium shadow-sm"
                         >
                              Đăng nhập
                         </button>
                    </div>
               </form>
               {/* Separator */}
               <div className="relative mt-6">
                    <div className="absolute inset-0 flex items-center">
                         <div className="w-full border-t border-gray-300"></div>
                    </div>
                    <div className="relative flex justify-center text-sm">
                         <span className="bg-white px-2 text-gray-500">Hoặc</span>
                    </div>
               </div>
               {/* Social login buttons */}
               <div className="flex space-x-2">
                    <button id="login-google" className="flex w-1/2 items-center justify-center space-x-2 rounded-lg px-4 py-2 text-sm font-medium text-gray-700 shadow-sm focus:outline-none">
                         <FontAwesomeIcon icon={faGoogleBrand} className="h-4 w-4" />
                         <span>Google</span>
                    </button>
                    <button id="login-facebook" className="flex w-1/2 items-center justify-center space-x-2 rounded-lg px-4 py-2 text-sm font-medium text-gray-700 shadow-sm focus:outline-none">
                         <FontAwesomeIcon icon={faFacebookBrand} className="h-4 w-4" />
                         <span>Facebook</span>
                    </button>
               </div>
               {/* Register link */}
               <div className="mt-6 text-center text-sm text-gray-500">
                    Bạn chưa có tài khoản?{' '}
                    <a href="#" className="font-medium text-blue-600 hover:text-blue-500">
                         Đăng ký ngay
                    </a>
               </div>
          </div>
     );
};

export default LoginForm;