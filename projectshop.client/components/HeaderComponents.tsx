"use client";
import { useState, type FormEvent } from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBars, faCartShopping, faMagnifyingGlass, faPhoneVolume, faTruckFast } from '@fortawesome/free-solid-svg-icons';
import HeaderAccount from "@/app/account/HeaderAccount";
import Image from "next/image";

function Header() {
     const [query, setQuery] = useState('');
     const [cartCount, setCartCount] = useState(0); // State for cart count
     const [isLoading, setIsLoading] = useState(false);

     const handleSearch = async (e: FormEvent) => {
          e.preventDefault();
          setIsLoading(true);
          // Simulate search delay
          await new Promise(resolve => setTimeout(resolve, 2000));
          console.log('Searching for:', query);
          setIsLoading(false);
     };

     const handleAddToCart = () => {
          setCartCount(prevCount => prevCount + 1); // For demo purposes
     };

     return (
          <div id="header-container" className="bg-white py-2 border-b border-gray-200 relative drop-shadow-md">
               <section className="container mx-auto px-4 lg:px-0 max-w-300">
                    <section className="flex flex-wrap justify-between items-center -mx-2 md:-mx-3">
                         {/* Mobile Header - hidden on md+ */}
                         <div className="w-full px-2 md:px-3 flex justify-between items-center md:hidden">
                              {/* Menu button */}
                              <div>
                                   <FontAwesomeIcon icon={faBars} className="main-color w-5 h-5" />
                              </div>
                              {/* Logo */}
                              <div>
                                   <Image src="/assets/images/icons/web_logo/light-novel-world.png" alt="Light Novel World Logo" className="h-10" width={160} height={40} loading="eager" />
                              </div>
                              {/* Cart */}
                              <div className="relative cursor-pointer" onClick={handleAddToCart}>
                                   <FontAwesomeIcon icon={faCartShopping} className="main-color w-5 h-5" />
                                   <div className="absolute top-0 right-0 transform translate-x-1/2 -translate-y-1/2 bg-red-500 text-white text-xs font-bold rounded-full h-5 w-5 flex items-center justify-center transition-all duration-300">
                                        {cartCount}
                                   </div>
                              </div>
                         </div>

                         {/* Desktop/Tablet Layout - hidden on mobile */}
                         <div className="hidden md:flex md:w-full lg:w-full items-center">
                              {/* Logo */}
                              <div className="w-auto px-2 lg:w-2/12">
                                   <Image src="/assets/images/icons/web_logo/light-novel-world.png" alt="Light Novel World Logo" className="h-12" width={160} height={48} />
                              </div>

                              {/* Search Bar */}
                              <div className="w-auto px-2 lg:w-5/12">
                                   <form onSubmit={handleSearch} className="flex border border-blue-300 rounded-lg overflow-hidden">
                                        <input
                                             type="text"
                                             name="query"
                                             className="flex-1 px-4 py-2 text-sm outline-none placeholder-gray-500"
                                             placeholder="Tìm kiếm sản phẩm..."
                                             value={query}
                                             onChange={(e) => setQuery(e.target.value)}
                                        />
                                        <button type="submit" className="px-5 transition-colors" disabled={isLoading}>
                                             {isLoading ? (
                                                  <div className="animate-spin rounded-full h-4 w-4 border-b-2 border-gray-900"></div>
                                             ) : (
                                                  <FontAwesomeIcon icon={faMagnifyingGlass} className="text-black! main-color w-5 h-5" />
                                             )}
                                        </button>
                                   </form>
                              </div>

                              {/* Right Side Icons */}
                              <div className="hidden lg:flex w-5/12 items-center text-sm px-2 h-full">
                                   {/* Hotline */}
                                   <div className="flex items-center px-3">
                                        <FontAwesomeIcon icon={faPhoneVolume} className="main-color w-5 h-5" />
                                        <div className="flex flex-col pl-2">
                                             <span>Hotline</span>
                                             <span className="font-bold main-color">032838xxxx</span>
                                        </div>
                                   </div>

                                   {/* Order Tracking */}
                                   <div className="flex items-center px-3 h-full">
                                        <FontAwesomeIcon icon={faTruckFast} className="main-color w-5 h-5" />
                                        <div className="flex flex-col pl-2">
                                             <span>Tra cứu</span>
                                             <span>đơn hàng</span>
                                        </div>
                                   </div>

                                   {/* Cart */}
                                   <div className="relative flex items-center space-x-2 cursor-pointer h-full" onClick={handleAddToCart}>
                                        <FontAwesomeIcon icon={faCartShopping} className="main-color w-5 h-5" />
                                        <div className="absolute -top-2 left-0 transform translate-x-1 -translate-y-2 bg-red-500 text-white text-xs font-bold rounded-full h-5 w-5 flex items-center justify-center transition-all duration-300">
                                             {cartCount}
                                        </div>
                                        <span>Giỏ hàng</span>
                                   </div>

                                   <HeaderAccount />
                              </div>
                         </div>
                    </section>
               </section>
          </div>
     );
};

export default Header;