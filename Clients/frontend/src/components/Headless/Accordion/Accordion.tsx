import { Disclosure } from '@headlessui/react';
import { MdKeyboardArrowDown } from 'react-icons/md';

export const Accordion = () => {
  return (
    // TO-DO: remove margin when layout is implemented
    <Disclosure>
      {({ open }) => (
        <>
          <Disclosure.Button className='flex w-full items-center justify-between px-2 py-2 text-sm font-medium shadow-xl mt-5'>
            Is team pricing available?
            <MdKeyboardArrowDown size='25px' className={open ? 'rotate-180 transform' : ''} />
          </Disclosure.Button>
          <Disclosure.Panel className='px-4 pt-4 pb-2 text-sm text-gray-500'>
            Yes! You can purchase a license that you can share with your entire team.
          </Disclosure.Panel>
        </>
      )}
    </Disclosure>
  );
};
