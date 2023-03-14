import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import { ApiProvider } from '@reduxjs/toolkit/query/react';

import App from './App';
import { apiSlice } from 'features/api/apiSlice';

import './index.css';

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <BrowserRouter>
      <ApiProvider api={apiSlice}>
        <App />
      </ApiProvider>
    </BrowserRouter>
  </React.StrictMode>,
);
