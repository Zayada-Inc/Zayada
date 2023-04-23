import { Modal as MantineModal } from '@mantine/core';

import { useModal } from 'components/Modal/hooks/useModal';

export const Modal = () => {
  const { handleClose, ModalContent, isOpen } = useModal();

  return (
    <>
      <MantineModal opened={isOpen} onClose={handleClose} title='Authentication'>
        {ModalContent && <ModalContent />}
      </MantineModal>
    </>
  );
};
