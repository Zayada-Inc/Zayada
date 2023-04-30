import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';

import store, { persistor } from 'store/store';
import App from 'App';
import { MantineProvider } from '@mantine/core';

import './index.css';

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <MantineProvider
          theme={{
            fontFamily: 'Poppins, sans-serif',
            colors: {
              secondaryColors: ['#FFFFFF', '#1E1E1E', '#FF8940', '#FFA064', '#2563EB'],
            },
            fontSizes: {
              xs: '0.6rem',
              sm: '0.75rem',
              md: '0.9rem',
              lg: '1.1rem',
              xl: '1.5rem',
            },
          }}
          withGlobalStyles
          withNormalizeCSS
        >
          <BrowserRouter>
            <Routes>
              <Route path='/*' element={<App />} />
            </Routes>
          </BrowserRouter>
        </MantineProvider>
      </PersistGate>
    </Provider>
  </React.StrictMode>,
);
