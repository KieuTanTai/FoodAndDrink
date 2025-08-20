import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faList, faFileInvoice, faNewspaper, faHeadset } from '@fortawesome/free-solid-svg-icons';
import { faFantasyFlightGames } from '@fortawesome/free-brands-svg-icons';

const SubHeader = () => {
     return (
          <section id="sub-header" className="flex items-center bg-white border-b border-gray-200 shadow-md">
               <div className="container mx-auto max-w-[75rem]">
                    <div className="grid grid-cols-5 h-10">
                         {/* Cột 1: Danh mục sản phẩm */}
                         <div className="relative group col-span-1 h-full">
                              <div className="flex justify-center items-center h-10 w-full bg-main-color rounded-t-md cursor-pointer">
                                   <FontAwesomeIcon icon={faList} className='!text-white' />
                                   <p className="ml-2 font-bold text-sm">Danh mục sản phẩm</p>
                              </div>
                              {/* Nav content */}
                              <nav className="absolute z-10 hidden group-hover:block bg-white w-full shadow-lg rounded-b-md">
                                   <div className="flex flex-col">
                                        <a href="#" className="flex items-center p-2 hover:bg-gray-100">
                                             <img src="./src/assets/images/icons/Other_icons/Manga.webp" alt="Iphone" width="20" />
                                             <p className="ml-2 text-sm font-bold">Iphone</p>
                                        </a>
                                        <a href="#" className="flex items-center p-2 hover:bg-gray-100">
                                             <img src="./src/assets/images/icons/Other_icons/LN.webp" alt="light novel" width="20" />
                                             <p className="ml-2 text-sm font-bold">Samsung</p>
                                        </a>
                                        <a href="#" className="flex items-center p-2 hover:bg-gray-100">
                                             <img src="./src/assets/images/icons/Other_icons/yang-icon-1.webp" alt="xiaomi" width="20" />
                                             <p className="ml-2 text-sm font-bold">Xiaomi</p>
                                        </a>
                                        <a href="#" className="flex items-center p-2 hover:bg-gray-100">
                                             <img src="./src/assets/images/icons/Other_icons/yang-icon-4.webp" alt="vivo iqoo" width="20" />
                                             <p className="ml-2 text-sm font-bold">Vivo Iqoo</p>
                                        </a>
                                   </div>
                              </nav>
                         </div>
                         {/* Các cột còn lại: Các mục liên kết */}
                         <div className="col-span-4 grid grid-cols-4 items-center h-full ">
                              <a href="#" className="flex items-center justify-center h-full hover:bg-gray-100">
                                   <FontAwesomeIcon icon={faFileInvoice} className='main-color' />
                                   <p className="ml-2 font-bold text-sm">Lịch sử mua hàng</p>
                              </a>
                              <a href="#" className="flex items-center justify-center h-full hover:bg-gray-100">
                                   <FontAwesomeIcon icon={faNewspaper} className='main-color' />
                                   <p className="ml-2 font-bold text-sm">Tin tức & Sự kiện</p>
                              </a>
                              <a href="#" className="flex items-center justify-center h-full hover:bg-gray-100">
                                   <FontAwesomeIcon icon={faFantasyFlightGames} className='main-color' />
                                   <p className="ml-2 font-bold text-sm">Mobile Phone Store</p>
                              </a>
                              <a href="#" className="flex items-center justify-center h-full hover:bg-gray-100">
                                   <FontAwesomeIcon icon={faHeadset} className='main-color' />
                                   <p className="ml-2 font-bold text-sm">Liên hệ</p>
                              </a>
                         </div>
                    </div>
               </div>
          </section>
     );
};

export default SubHeader;