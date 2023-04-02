import { useState } from 'react';
import { FiSettings } from 'react-icons/fi';
import { RxExit } from 'react-icons/rx';

import { Logo } from 'components/Logo';
import { Modal } from 'components/Headless';

export const Drawer = () => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div className='flex flex-col text-white bg-dark-color'>
      <div className='flex items-center justify-center h-[70px]'>
      <Logo isBlack={false} />
      </div>
      <div className='flex flex-col items-center justify-end gap-12 pb-12 grow'>
        <FiSettings size='25px' onClick={() => setIsOpen(!isOpen)} className='cursor-pointer' />
        <RxExit size='25px' />
        <Modal {...{ isOpen, setIsOpen }} />
      </div>
    </div>
  );
};
