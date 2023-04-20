import { useDispatch, useSelector } from 'react-redux';

import { selectModal, setModal } from 'store/slices/modal';
import { MODAL_TYPES } from 'components/Modal/constants';

export const useModal = () => {
  const { type, isOpen } = useSelector(selectModal);
  const dispatch = useDispatch();

  const handleClose = () => {
    dispatch(setModal({ type: '', isOpen: false }));
  };

  const ModalContent = MODAL_TYPES[type];

  return { isOpen, handleClose, ModalContent };
};
