import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEnvelope, faLock, faUser } from '@fortawesome/free-solid-svg-icons';
import { faFacebook as faFacebookBrand, faGoogle as faGoogleBrand } from '@fortawesome/free-brands-svg-icons';

const SignupForm: React.FC = () => {
     const [name, setName] = useState('');
     const [email, setEmail] = useState('');
     const [password, setPassword] = useState('');
     const [confirmPassword, setConfirmPassword] = useState('');

     const handleSubmit = (e: React.FormEvent) => {
          e.preventDefault();
          // Logic xử lý đăng ký tài khoản ở đây
          console.log('Tên:', name);
          console.log('Email:', email);
          console.log('Mật khẩu:', password);
          console.log('Nhập lại mật khẩu:', confirmPassword);

          if (password !== confirmPassword) {
               alert('Mật khẩu và xác nhận mật khẩu không khớp!');
               return;
          }

          alert('Đăng ký thành công!');
     };

     return (
          <div className="mx-auto flex max-w-[75rem] flex-col items-center justify-center p-4 lg:px-0">
               <div className="w-full max-w-lg p-8 space-y-6 bg-white rounded-xl shadow-lg border border-gray-200">
                    <div className="text-center">
                         <h2 className="text-2xl font-bold text-gray-900">Chào mừng đến với cửa hàng</h2>
                         <p className="mt-2 text-sm text-gray-500">Đăng ký tài khoản để tiếp tục</p>
                    </div>

                    <form className="space-y-4" onSubmit={handleSubmit}>
                         {/* Tên */}
                         <div>
                              <label htmlFor="name" className="sr-only">Họ và tên</label>
                              <div className="relative">
                                   <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                                        <FontAwesomeIcon icon={faUser} className="text-gray-400" />
                                   </div>
                                   <input
                                        id="name"
                                        name="name"
                                        type="text"
                                        autoComplete="name"
                                        required
                                        placeholder="Họ và tên"
                                        value={name}
                                        onChange={(e) => setName(e.target.value)}
                                        className="w-full pl-10 pr-3 py-2 border border-gray-300 rounded-lg shadow-sm placeholder-gray-400 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                                   />
                              </div>
                              <div className="mt-1 h-4 text-xs text-red-500" id="sg-name-error-msg"></div>
                         </div>

                         {/* Email */}
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
                              <div className="mt-1 h-4 text-xs text-red-500" id="sg-email-error-msg"></div>
                         </div>

                         {/* Mật khẩu */}
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
                                        autoComplete="new-password"
                                        required
                                        placeholder="Mật khẩu"
                                        value={password}
                                        onChange={(e) => setPassword(e.target.value)}
                                        className="w-full pl-10 pr-3 py-2 border border-gray-300 rounded-lg shadow-sm placeholder-gray-400 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                                   />
                              </div>
                              <div className="mt-1 h-4 text-xs text-red-500" id="sg-pass-error-msg"></div>
                         </div>

                         {/* Nhập lại mật khẩu */}
                         <div>
                              <label htmlFor="confirm-password" className="sr-only">Nhập lại mật khẩu</label>
                              <div className="relative">
                                   <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                                        <FontAwesomeIcon icon={faLock} className="text-gray-400" />
                                   </div>
                                   <input
                                        id="confirm-password"
                                        name="confirm-password"
                                        type="password"
                                        autoComplete="new-password"
                                        required
                                        placeholder="Nhập lại mật khẩu"
                                        value={confirmPassword}
                                        onChange={(e) => setConfirmPassword(e.target.value)}
                                        className="w-full pl-10 pr-3 py-2 border border-gray-300 rounded-lg shadow-sm placeholder-gray-400 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                                   />
                              </div>
                              <div className="mt-1 h-4 text-xs text-red-500" id="sg-confirm-pass-error-msg"></div>
                         </div>

                         {/* Register button */}
                         <div>
                              <button
                                   type="submit"
                                   className="flex justify-center w-full px-4 py-2 text-sm font-medium text-white bg-violet-600 border border-transparent rounded-lg shadow-sm hover:bg-violet-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-violet-500"
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
                              <span className="px-2 bg-white text-gray-500">Hoặc</span>
                         </div>
                    </div>

                    {/* Social login buttons */}
                    <div className="flex space-x-2">
                         <button className="flex items-center justify-center w-1/2 px-4 py-2 space-x-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-lg shadow-sm 
                                   hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-500" id="google-signup">
                              <FontAwesomeIcon icon={faGoogleBrand} className="h-4 w-4" />
                              <span>Google</span>
                         </button>
                         <button className="flex items-center justify-center w-1/2 px-4 py-2 space-x-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-lg shadow-sm 
                                   hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-500" id="facebook-signup">
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