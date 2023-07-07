import { Modal as MantineModal } from '@mantine/core';

import { useModal } from 'components/Modal/hooks/useModal';

export const Modal = () => {
  const { handleClose, ModalContent, isOpen, title } = useModal();

  return (
    <>
      <MantineModal opened={isOpen} onClose={handleClose} {...{ title }}>
        {ModalContent && <ModalContent />}
      </MantineModal>
    </>
  );
};
