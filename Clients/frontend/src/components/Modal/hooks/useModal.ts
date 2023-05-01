import { useDispatch, useSelector } from 'react-redux';

import { selectModal, setModal } from 'store/slices/modal';
import { MODAL_TYPES } from 'components/Modal/constants';

export const useModal = () => {
  const { type, isOpen } = useSelector(selectModal);
  const dispatch = useDispatch();

  const handleClose = () => {
    dispatch(setModal({ type: '', isOpen: false }));
  };

  const computeTitle = (arr: string[]): string => {
    return arr.reduce((prev: string, curr: string) => {
      if (curr.charAt(0) === curr.charAt(0).toUpperCase()) {
        return `${prev}${curr} `;
      }
      return `${prev}${curr.charAt(0).toUpperCase() + curr.slice(1)} `;
    }, '');
  };

  const ModalContent = MODAL_TYPES[type];
  const splitByUppercase = type.split(/(?=[A-Z])/);

  const title = computeTitle(splitByUppercase);

  return { isOpen, handleClose, ModalContent, title };
};
