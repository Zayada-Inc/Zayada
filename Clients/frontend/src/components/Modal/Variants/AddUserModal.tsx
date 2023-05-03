import { TextInput } from '@mantine/core';
import { Button } from 'components/Button';
import { useRegisterMutation } from 'features/api/apiSlice';
import { useState } from 'react';
import { useDispatch } from 'react-redux';
import { setModal } from 'store/slices/modal';

export const AddUserModal = () => {
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [username, setUsername] = useState<string>('');
  const [register] = useRegisterMutation();
  const dispatch = useDispatch();

  const handleSubmit = () => {
    try {
      register({ email, password, username, displayName: username });
      dispatch(setModal({ type: '', isOpen: false }));
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <>
      <TextInput
        placeholder='display name'
        label='Display name'
        withAsterisk
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />
      <TextInput
        placeholder='username'
        label='Username'
        withAsterisk
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />
      <TextInput
        placeholder='email'
        label='Email'
        withAsterisk
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />
      <TextInput
        placeholder='password'
        label='Password'
        withAsterisk
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <Button text='Submit' onClick={handleSubmit} />
    </>
  );
};
