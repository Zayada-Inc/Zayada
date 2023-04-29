import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from 'store/store';

type SearchState = {
  tables: {
    allUsers: {
      query: string;
      activePage: number;
    };
  };
};

const initialState: SearchState = {
  tables: {
    allUsers: {
      query: '',
      activePage: 1,
    },
  },
};

export const searchSlice = createSlice({
  name: 'search',
  initialState,
  reducers: {
    setAllUsersSearch: (state, action: PayloadAction<string>) => {
      state.tables.allUsers.query = action.payload;
    },
    setAllUsersPage: (state, action: PayloadAction<number>) => {
      state.tables.allUsers.activePage = action.payload;
    },
  },
});

export const getAllUsers = (state: RootState) => state.search.tables.allUsers;

export const { setAllUsersSearch, setAllUsersPage } = searchSlice.actions;
