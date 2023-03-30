import storage from 'redux-persist/lib/storage';
import {
  FLUSH,
  PAUSE,
  PERSIST,
  persistReducer,
  persistStore,
  PURGE,
  REGISTER,
  REHYDRATE,
} from 'redux-persist';
import { combineReducers, configureStore } from '@reduxjs/toolkit';

import { apiSlice } from 'features/api/apiSlice';
import { userSlice } from './slices/user';

const reducer = combineReducers({
  [apiSlice.reducerPath]: apiSlice.reducer,
  user: userSlice.reducer,
});

const persistConfig = {
  key: 'root',
  version: 1,
  storage,
  blacklist: [apiSlice.reducerPath],
};

const persistedReducer = persistReducer(persistConfig, reducer);

const store = configureStore({
  reducer: persistedReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    }).concat(apiSlice.middleware),
});

export const persistor = persistStore(store);

export type RootState = ReturnType<typeof store.getState>;

export default store;
