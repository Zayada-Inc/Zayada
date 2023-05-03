import { useState } from 'react';
import { useDispatch } from 'react-redux';
import { TextInput } from '@mantine/core';

import { Button } from 'components/Button';
import { useAddGymMutation } from 'features/api/apiSlice';
import { setModal } from 'store/slices/modal';

export const AddGymModal = () => {
  const [gymName, setGymName] = useState<string>('');
  const [gymAddress, setGymAddress] = useState<string>('');
  const [adminID, setAdminID] = useState<string>('');
  const [addGym] = useAddGymMutation();
  const dispatch = useDispatch();

  const handleSubmit = () => {
    try {
      addGym({ gymName, gymAddress, adminUserId: adminID });
      dispatch(setModal({ type: '', isOpen: false }));
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <>
      <TextInput
        placeholder='gym name'
        label='Name'
        withAsterisk
        value={gymName}
        onChange={(e) => setGymName(e.target.value)}
      />
      <TextInput
        placeholder='gym address'
        label='Address'
        withAsterisk
        value={gymAddress}
        onChange={(e) => setGymAddress(e.target.value)}
      />
      <TextInput
        placeholder='Admin id'
        label='Gym admin id'
        withAsterisk
        value={adminID}
        onChange={(e) => setAdminID(e.target.value)}
      />
      <Button text='Submit' onClick={handleSubmit} />
    </>
  );
};
