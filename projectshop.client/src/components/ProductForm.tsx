import React from 'react';

interface Product {
     id: number;
     imageSrc: string;
     altText: string;
     title: string;
     price: string;
     oldPrice: string;
     salePercentage: string;
}

interface ProductFormProps {
     product: Product;
}

const ProductForm: React.FC<ProductFormProps> = ({ product }) => {
     const { imageSrc, altText, title, salePercentage, price, oldPrice } = product;

     return (
          <div className="relative overflow-hidden rounded-lg shadow-md transition-all transform hover:scale-105 border-[1px] p-2 hover:border-blue-200 duration-300 cursor-pointer" id={`product-${product.id}`}>
               <div className="relative overflow-hidden">
                    <span className="block w-full h-full">
                         <img
                              src={imageSrc}
                              alt={altText}
                              className="w-full h-full object-cover"
                         />
                    </span>
                    <div className="absolute top-2 right-2 bg-red-500 text-xs font-bold px-2 py-1 rounded-sm">{salePercentage}</div>
               </div>

               <div className="p-4 bg-white text-center">
                    <h4 className="font-light capitalize text-base truncate mb-2" title={title}>{title}</h4>
                    <div>
                         <span className="price font-bold text-lg text-black">{price}</span>
                         <del className="price old-price pl-2 text-sm text-gray-500">{oldPrice}</del>
                    </div>
               </div>

               {/* Action buttons on hover */}
               <div className="flex justify-center space-x-2 mt-4">
                    <div className="add-to-cart">
                         <div className="button bg-green-500 hover:bg-green-600 py-2 px-2 rounded-md text-xs">
                              {/* <FontAwesomeIcon icon={faBasketShopping} size="lg" /> */}
                              <p>Thêm vào giỏ hàng</p>
                         </div>
                    </div>
                    <div className="buy-btn">
                         <div className="button bg-main-color py-2 px-2 rounded-md text-xs">
                              {/* <FontAwesomeIcon icon={faBagShopping} size="lg" /> */}
                              <p>Mua ngay</p>
                         </div>
                    </div>
               </div>
          </div>
     );
};

export default ProductForm;