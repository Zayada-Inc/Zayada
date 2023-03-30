import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from 'store/store';

interface userSliceState {
  user: {
    username: string;
    displayName: string;
    image: string;
  };
}

const initialState: userSliceState = {
  user: {
    username: '',
    displayName: '',
    image: '',
  },
};

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<userSliceState['user']>) => {
      state.user = action.payload;
    },
  },
});

export const selectUser = (state: RootState) => state.user.user;

export const { setUser } = userSlice.actions;
