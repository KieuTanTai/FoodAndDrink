import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEnvelope, faLock } from '@fortawesome/free-solid-svg-icons';
import { faFacebook as faFacebookBrand, faGoogle as faGoogleBrand } from '@fortawesome/free-brands-svg-icons';

const LoginForm: React.FC = () => {
     const [email, setEmail] = useState('');
     const [password, setPassword] = useState('');
     const [rememberMe, setRememberMe] = useState(false);

     const handleSubmit = (e: React.FormEvent) => {
          e.preventDefault();
          // Logic xử lý đăng nhập ở đây
          console.log('Email:', email);
          console.log('Mật khẩu:', password);
          console.log('Ghi nhớ tôi:', rememberMe);
     };

     return (
          <div className="mx-auto flex max-w-[75rem] flex-col items-center justify-center p-4 lg:px-0">
               <div className="w-full max-w-lg p-8 space-y-6 bg-white rounded-xl shadow-lg border border-gray-200">
                    <div className="text-center">
                         <h2 className="text-2xl font-bold text-gray-900">Chào mừng đến với cửa hàng</h2>
                         <p className="mt-2 text-sm text-gray-500">Đăng nhập tài khoản để tiếp tục</p>
                    </div>

                    <form className="space-y-4" onSubmit={handleSubmit} id="login-form">
                         {/* Email input */}
                         <div>
                              <label htmlFor="email" className="sr-only">Email</label>
                              <div className="relative">
                                   <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                                        <FontAwesomeIcon icon={faEnvelope} className="text-gray-400" />
                                   </div>
                                   <input
                                        id="email"
                                        name="email"
                                        type="email"
                                        autoComplete="email"
                                        required
                                        placeholder="Email"
                                        value={email}
                                        onChange={(e) => setEmail(e.target.value)}
                                        className="w-full pl-10 pr-3 py-2 border border-gray-300 rounded-lg shadow-sm placeholder-gray-400 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                                   />
                              </div>
                              <div className="mt-1 h-4 text-xs text-red-500" id="lg-email-error-msg"></div>
                         </div>

                         {/* Password input */}
                         <div>
                              <label htmlFor="password" className="sr-only">Mật khẩu</label>
                              <div className="relative">
                                   <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                                        <FontAwesomeIcon icon={faLock} className="text-gray-400" />
                                   </div>
                                   <input
                                        id="password"
                                        name="password"
                                        type="password"
                                        autoComplete="current-password"
                                        required
                                        placeholder="Mật khẩu"
                                        value={password}
                                        onChange={(e) => setPassword(e.target.value)}
                                        className="w-full pl-10 pr-3 py-2 border border-gray-300 rounded-lg shadow-sm placeholder-gray-400 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                                   />
                              </div>
                              <div className="mt-1 h-4 text-xs text-red-500" id="lg-pass-error-msg"></div>
                         </div>

                         {/* Remember me & Forgot password */}
                         <div className="flex items-center justify-between">
                              <div className="flex items-center">
                                   <input
                                        id="remember-me"
                                        name="remember-me"
                                        type="checkbox"
                                        checked={rememberMe}
                                        onChange={(e) => setRememberMe(e.target.checked)}
                                        className="w-4 h-4 text-blue-600 border-gray-300 rounded "
                                   />
                                   <label htmlFor="remember-me" className="ml-2 block text-sm text-gray-900">
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
                                   className="flex justify-center w-full px-4 py-2 text-sm font-medium border border-transparent rounded-lg shadow-sm 
                                             focus:outline-none"
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
                              <span className="px-2 bg-white text-gray-500">Hoặc</span>
                         </div>
                    </div>

                    {/* Social login buttons */}
                    <div className="flex space-x-2">
                         <button className="flex items-center justify-center w-1/2 px-4 py-2 space-x-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-lg shadow-sm 
                                   hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-500" id="google-login">
                              <FontAwesomeIcon icon={faGoogleBrand} className="h-4 w-4" />
                              <span>Google</span>
                         </button>
                         <button className="flex items-center justify-center w-1/2 px-4 py-2 space-x-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-lg shadow-sm 
                                   hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-500" id="facebook-login">
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
          </div>
     );
};

export default LoginForm;