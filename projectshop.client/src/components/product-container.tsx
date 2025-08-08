import React from 'react';
import ProductForm from './product-form';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBolt } from '@fortawesome/free-solid-svg-icons';

interface Product {
     id: number;
     imageSrc: string;
     altText: string;
     title: string;
     price: string;
     oldPrice: string;
     salePercentage: string;
     status: string; // change to enum after
}

interface ProductContainerProps {
     id: string; // optional for flexibility
     title: string;
     products: Product[];
     link: string;
}

const ProductContainer: React.FC<ProductContainerProps> = ({ id, title, products, link }) => {
     return (
          <section className="mx-auto flex max-w-[75rem] flex-col items-center justify-center p-4 lg:px-0" id={`container-${id}`}>
               <div className="bg-white p-4 shadow-sm rounded-lg">
                    <div className="flex items-center mb-6 border-b pb-2">
                         {/* Icon sẽ thay đổi tùy theo yêu cầu, ở đây dùng tạm một icon */}
                         <FontAwesomeIcon icon={faBolt} className="text-red-500 mr-2" />
                         <h2 className="uppercase font-bold text-xl">{title}</h2>
                    </div>

                    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-5 gap-4">
                         {products.map((product) => (
                              <ProductForm key={product.id} product={product} />
                         ))}
                    </div>

                    <div className="flex justify-center items-center font-bold capitalize mt-6">
                         <a href={link} className="bg-main-color text-gray-800 font-bold py-2 px-8 rounded-full transition-colors duration-200">
                              <p className='text-black'>Xem thêm</p>
                         </a>
                    </div>
               </div>
          </section>
     );
};

export default ProductContainer;