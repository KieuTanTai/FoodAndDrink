import Footer from "../components/FooterComponents";
import Header from "../components/HeaderComponents";
import SaleEventBlock from "../components/saleEvent/SaleEventBlock";
import SaleEventMainSlider from "../components/saleEvent/SaleEventMainSlider";
import SaleEventSideSlider from "../components/saleEvent/SaleEventSideSlider";
import SubHeader from "../components/SubHeader";
import { AbsoluteArrowNavigationProvider } from "../contexts/absoluteArrow/NavigationProvider";
import OtherInfoBlock from "../modal/components/others/OtherInfoBlock";
import type { SaleEventItemProps } from "../models/props/sale_events/sale-event-item-props";
import DemoPage from "./demo/pages_demo";

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
                <SaleEventBlock
                    mainBlock={
                        <AbsoluteArrowNavigationProvider value={{ saleEventItems: mainEvents }} timeInterval={5000}>
                            <SaleEventMainSlider saleEventItems={mainEvents} />
                        </AbsoluteArrowNavigationProvider>
                    }
                    sideBlocks={[
                        <AbsoluteArrowNavigationProvider value={{ saleEventItems: sideEvents }} timeInterval={3000} key="side-slider">
                            <SaleEventSideSlider saleEventItems={sideEvents} />
                        </AbsoluteArrowNavigationProvider>,

                        <AbsoluteArrowNavigationProvider value={{ saleEventItems: sideEvents }} timeInterval={3000} key="side-slider">
                            <SaleEventSideSlider saleEventItems={sideEvents} />
                        </AbsoluteArrowNavigationProvider>
                    ]}
                    
                />
                {/* Product Container */}
                {/* Repeat ProductContainer as needed */}
                {/* Other Info Block */}
                <OtherInfoBlock />
                <DemoPage />
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