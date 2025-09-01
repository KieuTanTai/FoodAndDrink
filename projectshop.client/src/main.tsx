import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './assets/css/index.css';
import Header from './components/header';
import Footer from './components/footer';
import SubHeader from './components/sub-header';
import ProductContainer from './components/product-container';
import LoginForm from './pages/login-page';
import ForgotPasswordForm from './pages/forgot-password-page';
import SignupForm from './pages/signup-page';
import Cart from './pages/cart-page';
import UserPage from './pages/user-page';
//import SaleEventBlock from './components/index-sale-event';

createRoot(document.getElementById('header')!).render(
  <StrictMode>
    <Header />
    <SubHeader />
  </StrictMode>
);
createRoot(document.getElementById('root')!).render(
  <StrictMode>
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
    <LoginForm />
    <ForgotPasswordForm onBackToLogin={() => { /* handle back to login */ }} />
    <SignupForm />
    <Cart />
    <UserPage></UserPage>
  </StrictMode>,
);

createRoot(document.getElementById('footer')!).render(
  <StrictMode>
    <Footer />
  </StrictMode>
);
