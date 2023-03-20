import { useState } from 'react';
import { LOGIN_FIELDS, Form } from 'components/Form';
import { Logo } from 'components/Logo';
import { ILoginRequest } from 'features/api/types';
import { useTranslation } from 'react-i18next';
import { Link, useNavigate } from 'react-router-dom';
import { IAPIError } from 'types';
import * as yup from 'yup';
import { useLoginMutation } from 'features/api/apiSlice';
import { useDispatch } from 'react-redux';
import { setUser } from 'store/slices/user';
import { loginSchema } from '../validation/validation ';
import { useLogin } from '../hooks/useLogin';
import { FormMessage } from '../components/FormMessage';

export const Login = () => {
  const { t, onSubmit, apiValidationError } = useLogin();

  const formMessage = (
    <div className='flex gap-1'>
      <p>{t('login.message.noAccount')}</p>
      <Link to='/register' className='text-blue-700'>
        {t('login.message.redirect')}
      </Link>
    </div>
  );

  return (
    <>
      <div className='flex h-screen border-green-400'>
        <div className='w-1/2 flex flex-col items-center gap-16 mt-16'>
          <div className='flex items-center gap-3'>
            <p className='text-xl font-bold'>{t('zayada')}</p>
            <Logo isBlack />
          </div>
          <p className='font-bold text-2xl'>{t('login.signIn')}</p>
          <div className='w-3/5'>
            <Form
              fields={LOGIN_FIELDS}
              {...{ onSubmit, apiValidationError }}
              schema={loginSchema}
              message={
                <FormMessage
                  linkTo='/register'
                  message={t('login.message.noAccount')}
                  linkedMessage={t('login.message.redirect')}
                />
              }
            />
          </div>
        </div>
        <div className='w-1/2 bg-brand-pattern'></div>
      </div>
    </>
  );
};
