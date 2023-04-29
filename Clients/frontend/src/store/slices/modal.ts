import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from 'store/store';

type ModalTypeState = {
  type: string;
  isOpen: boolean;
};

const initialState: ModalTypeState = {
  type: '',
  isOpen: false,
};

export const modalSlice = createSlice({
  name: 'modal',
  initialState,
  reducers: {
    setModal: (state, action: PayloadAction<ModalTypeState>) => {
      state.type = action.payload.type;
      state.isOpen = action.payload.isOpen;
    },
  },
});

export const selectModal = (state: RootState) => state.modal;

export const { setModal } = modalSlice.actions;
