import { faBolt, faFireAlt, faClock } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import ProductContainer from "../../components/product/ProductContainer";
import FlashSaleContainer from "../../components/product/FlashSaleProductContainer";
import ProductCard from "../../components/product/ProductCard";

// Danh sách sản phẩm thường
const products = [
  {
    id: "1",
    imageSrc: "https://i.imgur.com/cp5Pt4b.png",
    title: "Product 1",
    price: "$100",
    oldPrice: "$120",
    salePercentage: "17%",
  },
  {
    id: "2",
    imageSrc: "https://i.imgur.com/cp5Pt4b.png",
    title: "Product 2",
    price: "$80",
    oldPrice: "$100",
    salePercentage: "20%",
  },
];

// Danh sách sản phẩm flash sale (ví dụ, bạn có thể tách riêng)
const flashSaleProducts = [
  {
    id: "3",
    imageSrc: "https://i.imgur.com/cp5Pt4b.png",
    title: "Flash Sale 1",
    price: "$50",
    oldPrice: "$80",
    salePercentage: "38%",
  },
  {
    id: "4",
    imageSrc: "https://i.imgur.com/cp5Pt4b.png",
    title: "Flash Sale 2",
    price: "$40",
    oldPrice: "$70",
    salePercentage: "43%",
  },
];

// Component đếm ngược đơn giản (bạn có thể thay bằng countdown thực tế)
function Countdown() {
  return (
    <div className="text-red-600 font-bold flex items-center gap-1">
      <FontAwesomeIcon icon={faClock} />
      00:29:59
    </div>
  );
}

export default function DemoPage() {
  return (
    <>
      {/* Container Flash Sale */}
      <FlashSaleContainer
        id="flashsale"
        header={
          <div className="flex items-center gap-2 text-red-600 text-xl font-bold">
            <FontAwesomeIcon icon={faFireAlt} />
            FLASH SALE
          </div>
        }
        countdown={<Countdown />}
        footer={
          <button className="rounded-md transition-colors duration-200">
            Xem Thêm
          </button>
        }
      >
        {flashSaleProducts.map((product) => (
          <ProductCard
            key={product.id}
            {...product}
            onAddToCart={() => console.log(`[LOG] [FLASH SALE] Thêm sản phẩm vào giỏ: ${product.title}`)}
            onBuyNow={() => console.log(`[LOG] [FLASH SALE] Mua ngay sản phẩm: ${product.title}`)}
          />
        ))}
      </FlashSaleContainer>

      {/* Container Sản phẩm nổi bật */}
      <ProductContainer
        id="featured"
        header={
          <>
            <FontAwesomeIcon icon={faBolt} className="text-red-500 mr-2" />
            <h2 className="uppercase font-bold text-xl text-black">Featured Products</h2>
          </>
        }
        bodyBg="bg-white"
        footer={
          <a
            href="#"
            className="transition-colors duration-200"
          >
            <button className="text-black rounded-md">Xem Thêm</button>
          </a>
        }
      >
        {products.map((product) => (
          <ProductCard
            key={product.id}
            {...product}
            onAddToCart={() => console.log(`[LOG] Thêm sản phẩm vào giỏ: ${product.title}`)}
            onBuyNow={() => console.log(`[LOG] Mua ngay sản phẩm: ${product.title}`)}
          />
        ))}
      </ProductContainer>
    </>
  );
}