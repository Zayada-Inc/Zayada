import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { IAuthenticationResponse } from 'features/api/types';
import { RootState } from 'store/store';

type userSliceState = {
  data: IAuthenticationResponse;
};

const initialState: userSliceState = {
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
    setUser: (state, action: PayloadAction<userSliceState>) => {
      console.log(action.payload, 'userslice');
      state.data = action.payload.data;
    },
  },
});

export const selectUser = (state: RootState) => state.user.data;

export const { setUser } = userSlice.actions;
