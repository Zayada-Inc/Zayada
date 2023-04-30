import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { IAuthenticationResponse } from 'features/api/types';
import { RootState } from 'store/store';

type UserSliceState = {
  data: IAuthenticationResponse;
};

const initialState: UserSliceState = {
  data: {
    displayName: '',
    username: '',
    photos: [
      {
        url: '',
      },
    ],
  },
};

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<UserSliceState>) => {
      state.data = action.payload.data;
    },
  },
});

export const selectUser = (state: RootState) => state.user.data;

export const { setUser } = userSlice.actions;
