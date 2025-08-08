import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEnvelope, faLock, faUser } from '@fortawesome/free-solid-svg-icons';
import { faFacebook as faFacebookBrand, faGoogle as faGoogleBrand } from '@fortawesome/free-brands-svg-icons';

const SignupForm: React.FC = () => {
     const [name, setName] = useState('');
     const [email, setEmail] = useState('');
     const [password, setPassword] = useState('');
     const [confirmPassword, setConfirmPassword] = useState('');
     const [message, setMessage] = useState('');
     const [isError, setIsError] = useState(false);

     const handleSubmit = (e: React.FormEvent) => {
          e.preventDefault();
          setMessage('');
          setIsError(false);

          console.log('Tên:', name);
          console.log('Email:', email);
          console.log('Mật khẩu:', password);
          console.log('Nhập lại mật khẩu:', confirmPassword);

          if (password !== confirmPassword) {
               setMessage('Mật khẩu và xác nhận mật khẩu không khớp!');
               setIsError(true);
               return;
          }

          setMessage('Đăng ký thành công!');
          setIsError(false);
          // Logic xử lý đăng ký tài khoản ở đây
     };

     return (
          <div className="mx-auto flex max-w-[75rem] flex-col items-center justify-center p-4 lg:px-0">
               <div className="w-full max-w-lg space-y-6 rounded-xl border border-gray-200 bg-white p-8 shadow-lg">
                    <div className="text-center">
                         <h2 className="text-2xl font-bold text-gray-900">Chào mừng đến với cửa hàng</h2>
                         <p className="mt-2 text-sm text-gray-500">Đăng ký tài khoản để tiếp tục</p>
                    </div>

                    <form className="space-y-4" onSubmit={handleSubmit} id='signup-form'>
                         {/* Tên */}
                         <div>
                              <label htmlFor="signup-name" className="sr-only">Họ và tên</label>
                              <div className="relative">
                                   <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                                        <FontAwesomeIcon icon={faUser} className="text-gray-400" />
                                   </div>
                                   <input
                                        id="signup-name"
                                        name="signup_name"
                                        type="text"
                                        autoComplete="name"
                                        required
                                        placeholder="Họ và tên"
                                        value={name}
                                        onChange={(e) => setName(e.target.value)}
                                        className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none"
                                   />
                              </div>
                              <div className="mt-1 h-4 text-xs text-red-500" id="signup-name-error-msg"></div>
                         </div>

                         {/* Email */}
                         <div>
                              <label htmlFor="signup-email" className="sr-only">Email</label>
                              <div className="relative">
                                   <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                                        <FontAwesomeIcon icon={faEnvelope} className="text-gray-400" />
                                   </div>
                                   <input
                                        id="signup-email"
                                        name="signup_email"
                                        type="email"
                                        autoComplete="email"
                                        required
                                        placeholder="Email"
                                        value={email}
                                        onChange={(e) => setEmail(e.target.value)}
                                        className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none"
                                   />
                              </div>
                              <div className="mt-1 h-4 text-xs text-red-500" id="signup-email-error-msg"></div>
                         </div>

                         {/* Mật khẩu */}
                         <div>
                              <label htmlFor="signup-password" className="sr-only">Mật khẩu</label>
                              <div className="relative">
                                   <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                                        <FontAwesomeIcon icon={faLock} className="text-gray-400" />
                                   </div>
                                   <input
                                        id="signup-password"
                                        name="signup_password"
                                        type="password"
                                        autoComplete="new-password"
                                        required
                                        placeholder="Mật khẩu"
                                        value={password}
                                        onChange={(e) => setPassword(e.target.value)}
                                        className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none"
                                   />
                              </div>
                              <div className="mt-1 h-4 text-xs text-red-500" id="signup-pass-error-msg"></div>
                         </div>

                         {/* Nhập lại mật khẩu */}
                         <div>
                              <label htmlFor="signup-confirm-password" className="sr-only">Nhập lại mật khẩu</label>
                              <div className="relative">
                                   <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                                        <FontAwesomeIcon icon={faLock} className="text-gray-400" />
                                   </div>
                                   <input
                                        id="signup-confirm-password"
                                        name="confirm_password"
                                        type="password"
                                        autoComplete="new-password"
                                        required
                                        placeholder="Nhập lại mật khẩu"
                                        value={confirmPassword}
                                        onChange={(e) => setConfirmPassword(e.target.value)}
                                        className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none"
                                   />
                              </div>
                              <div className="mt-1 h-4 text-xs text-red-500" id="signup-confirm-pass-error-msg"></div>
                         </div>

                         {message && (
                              <div
                                   className={`p-3 text-center text-sm rounded-lg ${isError ? 'bg-red-100 text-red-700' : 'bg-green-100 text-green-700'}`}
                                   role="alert"
                              >
                                   {message}
                              </div>
                         )}

                         {/* Register button */}
                         <div>
                              <button
                                   type="submit"
                                   className="flex w-full justify-center rounded-lg border border-transparent px-4 py-2 text-sm font-medium shadow-sm"
                              >
                                   Đăng ký
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

                    {/* Social signup buttons */}
                    <div className="flex space-x-2">
                         <button id="signup-google" className="flex w-1/2 items-center justify-center space-x-2 rounded-lg px-4 py-2 text-sm font-medium text-gray-700 shadow-sm focus:outline-none">
                              <FontAwesomeIcon icon={faGoogleBrand} className="h-4 w-4" />
                              <span>Google</span>
                         </button>
                         <button id="signup-facebook" className="flex w-1/2 items-center justify-center space-x-2 rounded-lg px-4 py-2 text-sm font-medium text-gray-700 shadow-sm focus:outline-none">
                              <FontAwesomeIcon icon={faFacebookBrand} className="h-4 w-4" />
                              <span>Facebook</span>
                         </button>
                    </div>

                    {/* Login link */}
                    <div className="mt-6 text-center text-sm text-gray-500">
                         Đã có tài khoản?{' '}
                         <a href="#" className="font-medium text-blue-600 hover:text-blue-500">
                              Đăng nhập ngay
                         </a>
                    </div>
               </div>
          </div>
     );
};

export default SignupForm;