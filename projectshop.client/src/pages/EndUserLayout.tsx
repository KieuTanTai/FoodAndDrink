import Footer from "../components/FooterComponents";
import Header from "../components/HeaderComponents";
import ProductContainer from "../components/ProductContainer";
import SubHeader from "../components/SubHeader";

function EndUserLayout() {
    return (
        <>
            <header id="header" className="sticky top-0 z-50">
                <Header />
                <SubHeader />
            </header>

            <main id="main-content">
                {/*<SaleEventBlock/>*/}
                <ProductContainer
                    id="featured-products"
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