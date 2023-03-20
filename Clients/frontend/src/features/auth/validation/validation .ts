import * as yup from 'yup';

export const loginSchema = yup.object({
  Email: yup.string().required('Email is required').email('Email format is invalid'),
  Password: yup.string().required('Password is required'),
});

export const registerSchema = yup.object({
  Username: yup.string().required('Username is required'),
  Email: yup.string().required('Email is required').email('Email format is invalid'),
  DisplayName: yup.string().required('DisplayName is required'),
  Password: yup
    .string()
    .min(5, 'Password must be at least 5 characters')
    .test('isValid', 'Password is not valid', (value, context) => {
      if (value) {
        if (!/[A-Z]/.test(value)) {
          return context.createError({ message: 'Password must have an uppercase letter' });
        }
        if (!/[0-9]/.test(value))
          return context.createError({ message: 'Password must have a digit 0-9' });
        return true;
      }
    }),
});
