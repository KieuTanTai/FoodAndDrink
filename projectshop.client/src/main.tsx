import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './assets/css/index.css';
import App from './App';
import Header from './components/header';
import Footer from './components/footer';

createRoot(document.getElementById('header')!).render(
  <StrictMode>
    <Header />
  </StrictMode>
);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
);

createRoot(document.getElementById('footer')!).render(
  <StrictMode>
    <Footer />
  </StrictMode>
);
