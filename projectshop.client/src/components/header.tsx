import { useState } from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBars, faCartShopping, faMagnifyingGlass, faPhoneVolume, faTruckFast } from '@fortawesome/free-solid-svg-icons';
import { faCircleUser } from '@fortawesome/free-regular-svg-icons';

function Header() {
     const [query, setQuery] = useState('');
     const [cartCount, setCartCount] = useState(0); // State for cart count

     const handleSearch = (e: React.FormEvent) => {
          e.preventDefault();
          // Handle search logic here
          console.log('Searching for:', query);
     };

     const handleAddToCart = () => {
          setCartCount(prevCount => prevCount + 1); // For demo purposes
     };

     return (
          <header className="bg-white py-2 border-b border-gray-200 shadow-md">
               <section className="container mx-auto px-4 lg:px-0 max-w-[75rem]">
                    <section className="flex flex-wrap justify-between items-center -mx-2 md:-mx-3">
                         {/* Mobile Header - hidden on md+ */}
                         <div className="w-full px-2 md:px-3 flex justify-between items-center md:hidden">
                              {/* Menu button */}
                              <div>
                                   <FontAwesomeIcon icon={faBars} size="xl" className="main-color" />
                              </div>
                              {/* Logo */}
                              <div>
                                   <img src="./src/assets/images/icons/web_logo/light-novel-world.png" alt="Light Novel World Logo" className="h-10" />
                              </div>
                              {/* Cart */}
                              <div className="relative cursor-pointer" onClick={handleAddToCart}>
                                   <FontAwesomeIcon icon={faCartShopping} size="xl" className="main-color" />
                                   <div className="absolute top-0 right-0 transform translate-x-1/2 -translate-y-1/2 bg-red-500 text-white text-xs font-bold rounded-full h-5 w-5 flex items-center justify-center transition-all duration-300">
                                        {cartCount}
                                   </div>
                              </div>
                         </div>

                         {/* Desktop/Tablet Layout - hidden on mobile */}
                         <div className="hidden md:flex md:w-full lg:w-full items-center">
                              {/* Logo */}
                              <div className="w-auto px-2 lg:w-2/12">
                                   <img src="./src/assets/images/icons/web_logo/light-novel-world.png" alt="Light Novel World Logo" className="h-12" />
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
                                        <button type="submit" className="px-5 transition-colors">
                                             <FontAwesomeIcon icon={faMagnifyingGlass} className="!text-black main-color" />
                                        </button>
                                   </form>
                              </div>

                              {/* Right Side Icons */}
                              <div className="hidden lg:flex w-5/12 items-center text-sm px-2">
                                   {/* Hotline */}
                                   <div className="flex items-center px-3">
                                        <FontAwesomeIcon icon={faPhoneVolume} size="xl" className="main-color" />
                                        <div className="flex flex-col pl-2">
                                             <span>Hotline</span>
                                             <span className="font-bold main-color">032838xxxx</span>
                                        </div>
                                   </div>

                                   {/* Order Tracking */}
                                   <div className="flex items-center px-3">
                                        <FontAwesomeIcon icon={faTruckFast} size="xl" className="main-color" />
                                        <div className="flex flex-col pl-2">
                                             <span>Tra cứu</span>
                                             <span>đơn hàng</span>
                                        </div>
                                   </div>

                                   {/* Cart */}
                                   <div className="relative flex items-center space-x-2 cursor-pointer" onClick={handleAddToCart}>
                                        <FontAwesomeIcon icon={faCartShopping} size="xl" className="main-color" />
                                        <div className="absolute -top-2 left-0 transform translate-x-1 -translate-y-2 bg-red-500 text-white text-xs font-bold rounded-full h-5 w-5 flex items-center justify-center transition-all duration-300">
                                             {cartCount}
                                        </div>
                                        <span>Giỏ hàng</span>
                                   </div>

                                   {/* Account */}
                                   <div className="relative group pb-1">
                                        {/* Icon và text */}
                                        <div className="flex items-center pl-3 cursor-pointer">
                                             <FontAwesomeIcon icon={faCircleUser} size="xl" className="main-color" />
                                             <span className="pl-2">Tài khoản</span>
                                        </div>

                                        {/* Menu con */}
                                        <div className="absolute top-full right-0 mt-0 w-48 bg-white border border-gray-200 rounded-md shadow-lg hidden group-hover:block z-50">
                                             <div className="flex flex-col p-2">
                                                  <button className="py-2 px-4 flex items-center justify-center mb-2 text-left rounded-md font-medium">Đăng nhập</button>
                                                  <button className="py-2 px-4 flex items-center justify-center mb-2 text-left rounded-md font-medium">Đăng kí</button>
                                                  <button className="py-2 px-4 items-center justify-center text-left rounded-md hidden font-medium">Đăng xuất</button>
                                             </div>
                                        </div>
                                   </div>
                              </div>
                         </div>    
                    </section>
               </section>
          </header>
     );
};

export default Header;