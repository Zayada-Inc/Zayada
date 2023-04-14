import { useLoginMutation } from 'features/api/apiSlice';
import { ILoginRequest, IRegisterRequest } from 'features/api/types';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { setUser } from 'store/slices/user';
import { IAPIError } from 'types';

export const useLogin = () => {
  const [apiValidationError, setApiValidationError] = useState<IAPIError>();
  const [login] = useLoginMutation();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { t } = useTranslation();

  const onSubmit = async (data: ILoginRequest) => {
    try {
      const res = await login(data).unwrap();
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
