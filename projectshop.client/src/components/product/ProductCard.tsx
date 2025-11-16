import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBagShopping, faBasketShopping } from "@fortawesome/free-solid-svg-icons";

interface ProductCardProps {
     id: string;
     imageSrc: string;
     altText?: string;
     title: string;
     price: string;
     oldPrice?: string;
     salePercentage?: string;
     onAddToCart?: () => void;
     onBuyNow?: () => void;
}

export default function ProductCard({
     id,
     imageSrc,
     altText,
     title,
     price,
     oldPrice,
     salePercentage,
     onAddToCart,
     onBuyNow,
}: ProductCardProps) {
     return (
          <div
               className="relative flex flex-col overflow-hidden rounded-lg shadow-md transition-all transform hover:scale-105 border p-2 hover:border-blue-200 duration-300 cursor-pointer bg-white group"
               id={`product-${id}`}
          >
               <div className="relative">
                    <img src={imageSrc} alt={altText} className="w-full aspect-[4/5] object-cover" />
                    {salePercentage && (
                         <div className="absolute top-2 right-2 bg-red-500 text-xs font-bold px-2 py-1 rounded-sm text-white">
                              {salePercentage}
                         </div>
                    )}

                    {/* ACTION ICONS */}
                    <div
                         className="
            absolute bottom-2 left-1/2 -translate-x-1/2
            flex space-x-4 opacity-0 group-hover:opacity-100
            transition-opacity duration-300
            pointer-events-none group-hover:pointer-events-auto
            z-10
          "
                    >
                         <button
                              className="icon"
                              onClick={onBuyNow}
                              title="Mua ngay"
                              tabIndex={-1}
                         >
                              <FontAwesomeIcon icon={faBagShopping} />
                         </button>
                         <button
                              className="icon"
                              onClick={onAddToCart}
                              title="Thêm vào giỏ hàng"
                              tabIndex={-1}
                         >
                              <FontAwesomeIcon icon={faBasketShopping} />
                         </button>
                    </div>
               </div>
               {/* THÔNG TIN SẢN PHẨM */}
               <div className="text-center flex-1 flex flex-col justify-between mt-2">
                    <h4 className="font-light capitalize text-base truncate mb-2" title={title}>
                         {title}
                    </h4>
                    <div>
                         <span className="font-bold text-lg text-black">{price}</span>
                         {oldPrice && (
                              <del className="pl-2 text-sm text-gray-500">{oldPrice}</del>
                         )}
                    </div>
               </div>
          </div>
     );
}