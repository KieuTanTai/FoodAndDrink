import Header from './components/header';
import SubHeader from './components/sub-header';
import Footer from './components/footer';
import ProductContainer from './components/product-container';
import SignupForm from './pages/signup-page';

function App() {
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
                {/* <LoginForm /> */}
                {/* <ForgotPasswordForm onBackToLogin={() => { }} /> */}
                <SignupForm />
                {/* <Cart /> */}
                {/* <UserPage /> */}
            </main>
            <Footer />
        </>
    );     
}

export default App;