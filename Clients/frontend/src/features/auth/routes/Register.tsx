import { Form, REGISTRATION_FIELDS } from 'components/Form';
import { Logo } from 'components/Logo';
import { registerSchema } from '../validation/validation ';
import { useRegister } from '../hooks/useRegister';
import { FormMessage } from '../components/FormMessage';

export const Register = () => {
  const { t, onSubmit, apiValidationError } = useRegister();

  return (
    <>
      <div className='flex h-screen border-green-400'>
        <div className='w-1/2 flex flex-col items-center gap-16 mt-16'>
          <div className='flex items-center gap-3'>
            <p className='text-xl font-bold'>{t('zayada')}</p>
            <Logo isBlack />
          </div>
          <p className='font-bold text-2xl'>{t('register.signUp')}</p>
          <div className='w-3/5'>
            <p>mihnea123</p>
            <Form
              fields={REGISTRATION_FIELDS}
              {...{ onSubmit, apiValidationError }}
              schema={registerSchema}
              message={
                <FormMessage
                  linkTo='/login'
                  message={t('register.message.existingAccount')}
                  linkedMessage={t('register.message.redirect')}
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
