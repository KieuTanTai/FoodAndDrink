import './assets/css/index.css';
import React from 'react';
import { createRoot } from 'react-dom/client';
import App from './App';
import { MessageModalProvider } from './contexts/message/MessageModalProvider';

createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <MessageModalProvider headerId='header-container'>
      <App />
    </MessageModalProvider>
  </React.StrictMode>
);