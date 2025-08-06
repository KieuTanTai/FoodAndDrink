import { useState } from "react";

function Header() {
     const [query, setQuery] = useState('');

     const handleSearch = (e: React.FormEvent) => {
          e.preventDefault();
          // Handle search logic here
          console.log('Searching for:', query);
     };

     return (
          <header className="bg-white py-4 border-b border-gray-200 shadow-md">
               <section className="container mx-auto px-4 lg:px-0 max-w-[75rem]">
                    <section className="flex flex-wrap justify-between items-center -mx-2 md:-mx-3">
                         {/* Mobile Header - hidden on md+ */}
                         <div className="w-full px-2 md:px-3 flex justify-between items-center md:hidden">
                              {/* Menu button */}
                              <div>
                                   <i className="fa-solid fa-bars fa-xl"></i>
                              </div>
                              {/* Logo */}
                              <div>
                                   <img src="./src/assets/images/icons/web_logo/light-novel-world.png" alt="Light Novel World Logo" className="h-10" />
                              </div>
                              {/* Cart */}
                              <div className="relative">
                                   <i className="fa-solid fa-cart-shopping fa-xl"></i>
                                   <div className="absolute top-0 right-0 transform translate-x-1/2 -translate-y-1/2 bg-red-500 text-white text-xs rounded-full h-4 w-4 flex items-center justify-center">
                                        0
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
                              <div className="w-full px-2 lg:w-5/12">
                                   <form onSubmit={handleSearch} className="flex border border-blue-400 rounded-lg overflow-hidden">
                                        <input
                                             type="text"
                                             name="query"
                                             className="flex-1 px-4 py-2 text-sm outline-none placeholder-gray-500"
                                             placeholder="Tìm kiếm sản phẩm..."
                                             value={query}
                                             onChange={(e) => setQuery(e.target.value)}
                                        />
                                        <button type="submit" className="px-5 bg-blue-400 hover:bg-blue-500 transition-colors">
                                             <i className="fa-solid fa-magnifying-glass force-white"></i>
                                        </button>
                                   </form>
                              </div>

                              {/* Right Side Icons */}
                              <div className="hidden lg:flex w-5/12 items-center text-sm px-2">
                                   {/* Hotline */}
                                   <div className="flex items-center px-3">
                                        <i className="fa-solid fa-phone-volume fa-xl"></i>
                                        <div className="flex flex-col pl-2">
                                             <span>Hotline</span>
                                             <span className="font-bold main-color">032838xxxx</span>
                                        </div>
                                   </div>

                                   {/* Order Tracking */}
                                   <div className="flex items-center px-3">
                                        <i className="fa-solid fa-truck-fast fa-xl"></i>
                                        <div className="flex flex-col pl-2">
                                             <span>Tra cứu</span>
                                             <span>đơn hàng</span>
                                        </div>
                                   </div>

                                   {/* Cart */}
                                   <div className="relative flex items-center px-3">
                                        <i className="fa-solid fa-cart-shopping fa-xl"></i>
                                        <div className="absolute top-0 right-0 transform translate-x-1/2 -translate-y-1/2 bg-red-500 text-white text-xs rounded-full h-4 w-4 flex items-center justify-center">
                                             0
                                        </div>
                                        <span className="pl-2">Giỏ hàng</span>
                                   </div>

                                   {/* Account */}
                                   <div className="flex items-center pl-3">
                                        <i className="fa-regular fa-circle-user fa-xl"></i>
                                        <span className="pl-2">Tài khoản</span>
                                   </div>
                              </div>
                         </div>
                    </section>
               </section>
          </header>
     );
}

export default Header;