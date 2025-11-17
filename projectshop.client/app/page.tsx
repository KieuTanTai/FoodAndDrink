import Footer from "@/components/FooterComponents";
import Header from "@/components/HeaderComponents";
import ProductContainer from "@/components/product/ProductContainer";
import ProductCard from "@/components/product/ProductCard";
import SaleEventBlock from "@/components/saleEvent/SaleEventBlock";
import SaleEventMainSlider from "@/components/saleEvent/SaleEventMainSlider";
import SaleEventSideSlider from "@/components/saleEvent/SaleEventSideSlider";
import SubHeader from "@/components/SubHeader";
import { AbsoluteArrowNavigationProvider } from "@/contexts/absoluteArrow/NavigationProvider";
import OtherInfoBlock from "@/modal/components/others/OtherInfoBlock";
import type { SaleEventItemProps } from "@/props/sale_events/SaleEventItemProps";

// mock test view
const mainEvents: SaleEventItemProps[] = [
  {
    id: 1,
    title: "Big Sale Tháng 9",
    image: "https://images.unsplash.com/photo-1506744038136-46273834b3fb?w=800",
    description: "Khuyến mãi lớn cho toàn bộ sản phẩm tháng 9!",
    time: "23-09-2025 20:00",
  },
  {
    id: 2,
    title: "Black Friday",
    image: "https://images.unsplash.com/photo-1512436991641-6745cdb1723f?w=800",
    description: "Siêu giảm giá Black Friday, đừng bỏ lỡ!",
    time: "29-11-2025 00:00",
  }
];

const sideEvents: SaleEventItemProps[] = [
  {
    id: 2,
    title: "Deal Shock",
    image: "https://images.unsplash.com/photo-1465101046530-73398c7f28ca?w=400",
    time: "23-09-2025 21:00",
  },
  {
    id: 3,
    title: "Flash Sale",
    image: "https://images.unsplash.com/photo-1519125323398-675f0ddb6308?w=400",
    time: "24-09-2025 19:00",
  },
];

export default function Home() {
  return (
    <>
      <header id="header" className="sticky top-0 z-50">
        <Header />
        <SubHeader />
      </header>

      <main id="main-content">
        <SaleEventBlock
          mainBlock={
            <AbsoluteArrowNavigationProvider value={{ saleEventItems: mainEvents }} timeInterval={5000}>
              <SaleEventMainSlider saleEventItems={mainEvents} />
            </AbsoluteArrowNavigationProvider>
          }
          sideBlocks={[
            <AbsoluteArrowNavigationProvider value={{ saleEventItems: sideEvents }} timeInterval={3000} key="side-slider-1">
              <SaleEventSideSlider saleEventItems={sideEvents} />
            </AbsoluteArrowNavigationProvider>,

            <AbsoluteArrowNavigationProvider value={{ saleEventItems: sideEvents }} timeInterval={3000} key="side-slider-2">
              <SaleEventSideSlider saleEventItems={sideEvents} />
            </AbsoluteArrowNavigationProvider>
          ]}

        />
        {/* Other Info Block */}
        <OtherInfoBlock />

        {/* Fake Product Containers */}
        <div className="my-8">
          <ProductContainer id="hot-products" header={<h2 className="text-xl font-bold">Sản phẩm nổi bật</h2>}>
            <ProductCard id="1" imageSrc="/assets/images/iphone-15-pro-max-titan-tu-nhien.jpg.webp" title="iPhone 15 Pro Max" price="34.990.000₫" />
            <ProductCard id="2" imageSrc="/assets/images/samsung-galaxy-s24-fe-xanh-duong.jpg.webp" title="Samsung Galaxy S24 Ultra" price="29.990.000₫" />
            <ProductCard id="3" imageSrc="/assets/images/xiaomi-14-ultra-den.jpg.webp" title="Xiaomi 14 Ultra" price="24.990.000₫" />
          </ProductContainer>
        </div>
        <div className="my-8">
          <ProductContainer id="flash-sale" header={<h2 className="text-xl font-bold text-red-500">Flash Sale Hôm Nay</h2>}>
            <ProductCard id="4" imageSrc="/assets/images/realme-gt6-5g-china-den.jpg.webp" title="Realme GT6 5G" price="10.990.000₫" />
            <ProductCard id="5" imageSrc="/assets/images/oppo-find-x7-ultra-xanh.jpg.webp" title="Oppo Find X7 Ultra" price="18.990.000₫" />
            <ProductCard id="6" imageSrc="/assets/images/vivo-x100-pro-5g-xanh.jpg.webp" title="Vivo X100 Pro" price="20.990.000₫" />
          </ProductContainer>
        </div>
        <div className="my-8">
          <ProductContainer id="laptop" header={<h2 className="text-xl font-bold">Laptop giá tốt</h2>}>
            <ProductCard id="7" imageSrc="https://cdn.tgdd.vn/Products/Images/44/299250/macbook-air-m2-2022-16gb-512gb-600x600.jpg" title="MacBook Air M2" price="24.990.000₫" />
            <ProductCard id="8" imageSrc="https://cdn.tgdd.vn/Products/Images/44/299250/dell-xps-13-plus-9320-i7-600x600.jpg" title="Dell XPS 13 Plus" price="32.990.000₫" />
            <ProductCard id="9" imageSrc="https://cdn.tgdd.vn/Products/Images/44/299250/asus-zenbook-14-oled-ux3402za-i5-600x600.jpg" title="Asus Zenbook 14 OLED" price="18.990.000₫" />
          </ProductContainer>
        </div>
      </main>
      <footer id="footer" className="mt-10">
        <Footer />
      </footer>
    </>
  );
}
