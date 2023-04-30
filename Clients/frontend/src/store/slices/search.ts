import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from 'store/store';

export type queryPage = {
  query: string;
  activePage: number;
};

type SearchState = {
  tables: {
    allUsers: queryPage;
    gym: queryPage;
    pt: queryPage;
  };
};

const initialState: SearchState = {
  tables: {
    allUsers: {
      query: '',
      activePage: 1,
    },
    gym: {
      query: '',
      activePage: 1,
    },
    pt: {
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
    setGymSearch: (state, action: PayloadAction<string>) => {
      state.tables.gym.query = action.payload;
    },
    setGymPage: (state, action: PayloadAction<number>) => {
      state.tables.gym.activePage = action.payload;
    },
    setPtSearch: (state, action: PayloadAction<string>) => {
      state.tables.pt.query = action.payload;
    },
    setPtPage: (state, action: PayloadAction<number>) => {
      state.tables.pt.activePage = action.payload;
    },
  },
});

export const getAllUsers = (state: RootState) => state.search.tables.allUsers;
export const getAllGym = (state: RootState) => state.search.tables.gym;
export const getAllPt = (state: RootState) => state.search.tables.pt;

export const {
  setAllUsersSearch,
  setAllUsersPage,
  setGymPage,
  setGymSearch,
  setPtPage,
  setPtSearch,
} = searchSlice.actions;
