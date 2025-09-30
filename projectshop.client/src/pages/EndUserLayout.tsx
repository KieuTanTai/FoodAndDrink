import Footer from "../components/FooterComponents";
import Header from "../components/HeaderComponents";
import ProductContainer from "../components/ProductContainer";
import SaleEventBlock from "../components/saleEvent/SaleEventBlock";
import SubHeader from "../components/SubHeader";
import { AbsoluteArrowNavigationProvider } from "../context/absoluteArrow/AbsoluteArrowNavigationProvider";
import type { SaleEventItemProps } from "../models/props/sale_events/sale-event-item-props";

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
    // ... thêm event khác
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
// ... thêm event khác
  ];

function EndUserLayout() {

    return (
        <>
            <header id="header" className="sticky top-0 z-50">
                <Header />
                <SubHeader />
            </header>

            <main id="main-content">
                // TODO: CREATE PROVIDER AND FIX BUG HERE
                <AbsoluteArrowNavigationProvider value={{ isHaveSideEvent: true, mainSaleEvents: mainEvents, sideSaleEvents: sideEvents }}>
                    <SaleEventBlock mainSaleEvents={mainEvents} sideSaleEvents={sideEvents} />
                </AbsoluteArrowNavigationProvider>
                <ProductContainer
                    id="temp1-products"
                    title="Featured Products"
                    link="/products"
                    products={[
                        {
                            id: 1,
                            imageSrc: "./src/assets/images/1747247649_iphone-14-pro-max-den-cu.jpg.webp",
                            altText: "Product 1",
                            title: "Product 1",
                            price: "$100",
                            oldPrice: "$120",
                            salePercentage: "17%",
                            status: "available"
                        },
                        {
                            id: 2,
                            imageSrc: "./src/assets/images/1747247649_iphone-14-pro-max-den-cu.jpg.webp",
                            altText: "Product 2",
                            title: "Product 2",
                            price: "$80",
                            oldPrice: "$100",
                            salePercentage: "20%",
                            status: "out_of_stock"
                        }
                    ]}
                />
                
                <ProductContainer
                    id="temp2-products"
                    title="Featured Products"
                    link="/products"
                    products={[
                        {
                            id: 1,
                            imageSrc: "./src/assets/images/1747247649_iphone-14-pro-max-den-cu.jpg.webp",
                            altText: "Product 1",
                            title: "Product 1",
                            price: "$100",
                            oldPrice: "$120",
                            salePercentage: "17%",
                            status: "available"
                        },
                        {
                            id: 2,
                            imageSrc: "./src/assets/images/1747247649_iphone-14-pro-max-den-cu.jpg.webp",
                            altText: "Product 2",
                            title: "Product 2",
                            price: "$80",
                            oldPrice: "$100",
                            salePercentage: "20%",
                            status: "out_of_stock"
                        }
                    ]}
                />

                <ProductContainer
                    id="temp3-products"
                    title="Featured Products"
                    link="/products"
                    products={[
                        {
                            id: 1,
                            imageSrc: "./src/assets/images/1747247649_iphone-14-pro-max-den-cu.jpg.webp",
                            altText: "Product 1",
                            title: "Product 1",
                            price: "$100",
                            oldPrice: "$120",
                            salePercentage: "17%",
                            status: "available"
                        },
                        {
                            id: 2,
                            imageSrc: "./src/assets/images/1747247649_iphone-14-pro-max-den-cu.jpg.webp",
                            altText: "Product 2",
                            title: "Product 2",
                            price: "$80",
                            oldPrice: "$100",
                            salePercentage: "20%",
                            status: "out_of_stock"
                        }
                    ]}
                />

                <ProductContainer
                    id="temp4-products"
                    title="Featured Products"
                    link="/products"
                    products={[
                        {
                            id: 1,
                            imageSrc: "./src/assets/images/1747247649_iphone-14-pro-max-den-cu.jpg.webp",
                            altText: "Product 1",
                            title: "Product 1",
                            price: "$100",
                            oldPrice: "$120",
                            salePercentage: "17%",
                            status: "available"
                        },
                        {
                            id: 2,
                            imageSrc: "./src/assets/images/1747247649_iphone-14-pro-max-den-cu.jpg.webp",
                            altText: "Product 2",
                            title: "Product 2",
                            price: "$80",
                            oldPrice: "$100",
                            salePercentage: "20%",
                            status: "out_of_stock"
                        }
                    ]}
                />
                {/* <LoginPage /> */}
                {/* <ForgotPasswordPage onBackToLogin={() => { }} /> */}
                {/* <Cart /> */}
                {/* <UserPage /> */}
            </main>
            <footer id="footer" className="mt-10">
                <Footer />
            </footer>
        </>
    );
}

export default EndUserLayout;