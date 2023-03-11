import i18next from 'i18next';
import { Dispatch, SetStateAction } from 'react';
import { Dialog } from '@headlessui/react';
import { useTranslation } from 'react-i18next';

export interface ModalProps {
  isOpen: boolean;
  setIsOpen: Dispatch<SetStateAction<boolean>>;
}

export const Modal = ({ isOpen, setIsOpen }: ModalProps) => {
  const { t } = useTranslation();

  // temporary
  const handleGB = () => i18next.changeLanguage('en');
  const handleRO = () => i18next.changeLanguage('ro');
  // TO-DO: create generic modal for future use
  return (
    <Dialog open={isOpen} onClose={() => setIsOpen(false)} className='relative z-5'>
      <div className='fixed inset-0 bg-black/30' aria-hidden='true' />
      <div className='fixed inset-0 flex items-center justify-center p-4'>
        <Dialog.Panel className='w-full max-w-lg bg-white rounded pl-4'>
          <Dialog.Title className='text-xl font-semibold mt-2'>
            {t('modal.settings.title')}
          </Dialog.Title>
          <Dialog.Description className='text-gray-500 text-sm'>
            {t('modal.settings.description')}
          </Dialog.Description>

          <div className='flex gap-3'>
            <p className=''>{t('modal.settings.language')}</p>
            <button onClick={handleRO}>🇷🇴</button>
            <button onClick={handleGB}>🇬🇧</button>
          </div>

          <button onClick={() => setIsOpen(false)}>Cancel</button>
        </Dialog.Panel>
      </div>
    </Dialog>
  );
};
