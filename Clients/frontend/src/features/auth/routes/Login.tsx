import { Link } from 'react-router-dom';

import { LOGIN_FIELDS, Form } from 'components/Form';
import { FormMessage } from 'features/auth/components/FormMessage';
import { Logo } from 'components/Logo';
import { loginSchema } from 'features/auth/validation/validation ';
import { useLogin } from 'features/auth/hooks/useLogin';

export const Login = () => {
  const { t, onSubmit, apiValidationError } = useLogin();

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
