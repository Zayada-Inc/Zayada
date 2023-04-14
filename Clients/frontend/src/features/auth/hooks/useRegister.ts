import { useRegisterMutation } from 'features/api/apiSlice';
import { IRegisterRequest } from 'features/api/types';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { setUser } from 'store/slices/user';
import { IAPIError } from 'types';

export const useRegister = () => {
  const [apiValidationError, setApiValidationError] = useState<IAPIError>();
  const [register] = useRegisterMutation();
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { t } = useTranslation();

  const onSubmit = async (data: IRegisterRequest) => {
    try {
      const res = await register(data).unwrap();
      const user = Object.assign({}, res);
      delete user.token;

      dispatch(setUser({ data: user }));

      if (res?.token) {
        localStorage.setItem('token', res.token);
        navigate('/dashboard');
      }
    } catch (err: unknown) {
      console.log(err);

      setApiValidationError(err as IAPIError);
    }
  };

  return { onSubmit, apiValidationError, t };
};
