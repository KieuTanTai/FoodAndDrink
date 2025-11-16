import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMapLocationDot, faPhone, faShield } from '@fortawesome/free-solid-svg-icons';
import { faEnvelope } from '@fortawesome/free-regular-svg-icons';

const Footer = () => {
     return (
          <footer id="footer-container" className="bg-gray-200 py-8 border-t border-gray-200 shadow-[0_-4px_6px_-1px_rgb(0,0,0,0.1),_0_-2px_4px_-2px_rgb(0,0,0,0.1)] ">
               <section className="container mx-auto max-w-[75rem]">
                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 px-4">
                         {/* Column 1: Logo & Contact Info */}
                         <div className="col-span-full md:col-span-1 lg:col-span-1">
                              <div className="mb-6">
                                   <a href="#">
                                        <img src="./src/assets/images/icons/web_logo/light-novel-world.png" alt="Mobile Phone Store" className="h-12" />
                                   </a>
                              </div>
                              <div className="text-gray-600">
                                   <div className="flex items-start mb-4">
                                        <FontAwesomeIcon icon={faMapLocationDot} size="xl" className="mt-1 main-color" />
                                        <div className="text-sm leading-relaxed pl-4">
                                             <p>Số 110 Nguyễn Ngọc Nại, Khương Mai, Thanh Xuân, Hà Nội</p>
                                        </div>
                                   </div>
                                   <div className="flex items-center mb-4">
                                        <FontAwesomeIcon icon={faPhone} size="xl" className='main-color'/>
                                        <p className="pl-4 text-sm">Hotline: 03 2838 xxxx</p>
                                   </div>
                                   <div className="flex items-center mb-4">
                                        <FontAwesomeIcon icon={faEnvelope} size="xl" className='main-color'/>
                                        <p className="pl-4 text-sm">tanthi***la**@gmail.com</p>
                                   </div>
                                   <div className="flex items-center">
                                        <FontAwesomeIcon icon={faShield} size="xl" className='main-color'/>
                                        <p className="pl-4 text-sm">
                                             Giấy phép DKKD số xxxxxx, cấp lần thứ x năm 2024
                                        </p>
                                   </div>
                              </div>
                         </div>

                         {/* Column 2: Customer Support & Policies */}
                         <div className="col-span-full md:col-span-1 lg:col-span-1">
                              <div className="flex justify-between mb-4">
                                   <div className="mb-6">
                                        <h4 className="uppercase font-bold mb-4 text-gray-800 text-sm">Hỗ trợ khách hàng</h4>
                                        <div className="flex flex-col text-gray-600 space-y-2 text-sm">
                                             <a href="#" className="hover:text-blue-500">Câu hỏi thường gặp</a>
                                             <a href="#" className="hover:text-blue-500">Điều khoản dịch vụ</a>
                                        </div>
                                   </div>
                                   <div>
                                        <h4 className="uppercase font-bold mb-4 text-gray-800 text-sm">Chính sách</h4>
                                        <div className="flex flex-col text-gray-600 space-y-2 text-sm">
                                             <a href="#" className="hover:text-blue-500">Chính sách bảo mật</a>
                                             <a href="#" className="hover:text-blue-500">Chính sách thanh toán</a>
                                             <a href="#" className="hover:text-blue-500">Chính sách vận chuyển</a>
                                             <a href="#" className="hover:text-blue-500">Chính sách đổi trả</a>
                                        </div>
                                   </div>
                              </div>

                              <div>
                                   <h4 className="uppercase font-bold mb-4 text-gray-800 text-sm">các phương thức thanh toán</h4>
                                   <div className="flex flex-wrap gap-2">
                                        <img src="./src/assets/images/icons/payment_methods/internet_banking.jpg" alt="internet banking" className="h-10" />
                                        <img src="./src/assets/images/icons/payment_methods/pay_7.jpg" alt="visa" className="h-10" />
                                        <img src="./src/assets/images/icons/payment_methods/mastercard.jpg" alt="master card" className="h-10" />
                                        <img src="./src/assets/images/icons/payment_methods/pay_5.jpg" alt="tiền mặt" className="h-10" />
                                        <img src="./src/assets/images/icons/payment_methods/pay_4.jpg" alt="zalo pay" className="h-10" />
                                        <img src="./src/assets/images/icons/payment_methods/pay_8.jpg" alt="Momo" className="h-10" />
                                   </div>
                              </div>
                         </div>

                         {/* Column 3: Newsletter & Payment Methods */}
                         <div className="col-span-full lg:col-span-1">
                              <h4 className="uppercase font-bold mb-4 text-gray-800 text-sm">đăng ký nhận tin</h4>
                              <p className="text-sm text-gray-600 mb-4 max-w-sm">
                                   Hãy nhập email của bạn vào ô dưới đây để có thể nhận được tất cả tin tức mới nhất của LNW
                              </p>
                              <div className="flex flex-col md:flex-row items-center mb-6">
                                   <div>
                                        <img src="./src/assets/images/icons/web_logo/bct_img.jpg" alt="bo cong thuong" className="mt-4 md:mt-0 w-40 mb-2" />
                                        <form method="post" className="flex-1 w-full md:mr-4" id='newsletter-form'>
                                             <div className="flex">
                                                  <input
                                                       type="email"
                                                       name="user-email"
                                                       id="user-email"
                                                       placeholder="đăng ký email"
                                                       autoComplete="on"
                                                       required
                                                       className="flex-1 px-4 py-2 border bg-white border-gray-300 rounded-l-lg outline-none focus:border-transparent"
                                                  />
                                                  <button
                                                       type="submit"
                                                       className="uppercase px-4 py-2 font-semibold rounded-r-lg transition-colors"
                                                       name="subscribe"
                                                       id="subscribe"
                                                  >
                                                       đăng ký
                                                  </button>
                                             </div>
                                             <div className="mt-1 h-4 text-xs text-red-500" id="footer-newsletter-error-msg"></div>
                                        </form>
                                   </div>
                              </div>

                              <div className="mb-6">
                                   <h4 className="uppercase font-bold mb-4 text-gray-800 text-sm">kết nối với chúng tôi</h4>
                                   <div className="flex space-x-3">
                                        <a href="https://www.facebook.com/profile.php?id=100079218978533" target="_blank" rel="noopener noreferrer">
                                             <img src="./src/assets/images/icons/Social_media/facebook.svg" alt="facebook" className="h-12 w-12" />
                                        </a>
                                        <a href="https://www.tiktok.com/@tlangvotri" target="_blank" rel="noopener noreferrer">
                                             <img src="./src/assets/images/icons/Social_media/icons8-tiktok.svg" alt="tiktok" className="h-12 w-12" />
                                        </a>
                                        <a href="https://twitter.com/thien8224" target="_blank" rel="noopener noreferrer">
                                             <img src="./src/assets/images/icons/Social_media/icons8-twitterx.svg" alt="Twitter/X" className="h-12 w-12" />
                                        </a>
                                        <a href="https://chat.zalo.me/" target="_blank" rel="noopener noreferrer">
                                             <img src="./src/assets/images/icons/Social_media/icons8-zalo.svg" alt="zalo" className="h-12 w-12" />
                                        </a>
                                   </div>
                              </div>
                         </div>
                    </div>
               </section>
          </footer>
     );
};

export default Footer;