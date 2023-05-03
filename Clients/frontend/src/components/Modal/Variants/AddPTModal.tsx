import { useState } from 'react';
import { useDispatch } from 'react-redux';
import { TextInput } from '@mantine/core';

import { Button } from 'components/Button';
import { useAddPersonalTrainerMutation } from 'features/api/apiSlice';
import { setModal } from 'store/slices/modal';

export const AddPTModal = () => {
  const [certifications, setCertifications] = useState<string>('');
  const [userID, setUserID] = useState<string>('');
  const [gymID, setGymID] = useState<string>('');
  const [addPersonalTrainer] = useAddPersonalTrainerMutation();
  const dispatch = useDispatch();

  const handleSubmit = () => {
    try {
      addPersonalTrainer({ certifications, userId: userID, gymId: gymID });
      dispatch(setModal({ type: '', isOpen: false }));
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <>
      <TextInput
        placeholder='certifications'
        label='Certifications'
        withAsterisk
        value={certifications}
        onChange={(e) => setCertifications(e.target.value)}
      />
      <TextInput
        placeholder='user id'
        label='User ID'
        withAsterisk
        value={userID}
        onChange={(e) => setUserID(e.target.value)}
      />
      <TextInput
        placeholder='gym id'
        label='Gym ID'
        withAsterisk
        value={gymID}
        onChange={(e) => setGymID(e.target.value)}
      />
      <Button text='Submit' onClick={handleSubmit} />
    </>
  );
};
