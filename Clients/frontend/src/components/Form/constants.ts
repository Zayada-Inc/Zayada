import { FormField } from './Form';

export const REGISTRATION_FIELDS: FormField[] = [
  {
    name: 'Username',
    placeholder: 'Username',
    type: 'text',
  },
  {
    name: 'Email',
    placeholder: 'Email',
    type: 'email',
  },
  {
    name: 'DisplayName',
    placeholder: 'DisplayName',
    type: 'text',
  },
  {
    name: 'Password',
    placeholder: 'Password',
    type: 'text',
  },
];

export const LOGIN_FIELDS: FormField[] = [
  {
    name: 'Email',
    placeholder: 'Email',
    type: 'email',
  },
  {
    name: 'Password',
    placeholder: 'Password',
    type: 'text',
  },
];
