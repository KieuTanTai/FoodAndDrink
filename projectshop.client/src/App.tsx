import Modal from "react-modal";
import { Routes, Route, BrowserRouter } from 'react-router-dom';
import EndUserLayout from './pages/EndUserLayout';

function App() {
    //auth  

    Modal.setAppElement('#root');
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/*" element={<EndUserLayout />} />
                {/* <Route path="/admin/*" element={<AdminLayout />} /> */}
            </Routes>
        </BrowserRouter>
    );

}

export default App;