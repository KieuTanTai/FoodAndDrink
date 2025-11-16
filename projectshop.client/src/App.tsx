import Modal from "react-modal";
import { Routes, Route, BrowserRouter } from 'react-router-dom';
import EndUserLayout from './pages/EndUserLayout';
import CartPage from "./pages/CartPage";

function App() {
    //auth  

    Modal.setAppElement('#root');
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/*" element={<EndUserLayout />} />
                {/* <Route path="/admin/*" element={<AdminLayout />} /> */}
                <Route path="/cart" element={<CartPage />} />
            </Routes>
        </BrowserRouter>
    );

}

export default App;