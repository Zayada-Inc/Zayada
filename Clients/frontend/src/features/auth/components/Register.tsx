import { useNavigate } from 'react-router-dom';

import { useRegisterMutation } from 'features/api/apiSlice';
import { Form, REGISTRATION_FIELDS } from 'components/Form';

export const Register = () => {
  const navigate = useNavigate();
  const [register] = useRegisterMutation();

  const onSubmit = async (data: { [key: string]: string }) => {
    const res: any = await register(data);

    if (res?.data?.token) {
      localStorage.setItem('token', res.data.token);
      navigate('/dashboard');
    }
  };

  return (
    <>
      <div className='flex h-screen border-green-400'>
        <div className='border-2 border-red-500 w-1/2 '>
          <Form fields={REGISTRATION_FIELDS} {...{ onSubmit }} />
        </div>
        <div className='border-2 border-red-500 w-1/2'></div>
      </div>
    </>
  );
};
