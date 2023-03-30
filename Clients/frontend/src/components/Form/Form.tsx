import { FC } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';

import { Input } from 'components/Form';
import { IAPIError } from 'types';

export type FormField = {
  name: string;
  placeholder: string;
  type: 'text' | 'email' | 'password' | 'number';
};

interface FormProps {
  fields: FormField[];
  onSubmit: (data: any) => void;
  schema: any;
  apiValidationError: IAPIError | undefined;
  message?: JSX.Element;
}

export const Form: FC<FormProps> = ({ fields, onSubmit, schema, message, apiValidationError }) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({ resolver: yupResolver(schema) });

  return (
    <form onSubmit={handleSubmit(onSubmit)} className='flex flex-col gap-4'>
      {fields.map((field, i: number) => (
        <Input
          key={i}
          name={field.name}
          label={field.name}
          type={field.type}
          error={errors[field.name]}
          {...{ register, apiValidationError }}
        />
      ))}
      {message && <p>{message}</p>}
      {apiValidationError && (
        <p className='text-red-500 text-sm'>{apiValidationError.data.message}</p>
      )}
      {/* TO-DO: replace with btn component */}
      <input type='submit' className='bg-blue-600 text-white py-2 mt-20 rounded-md' />
    </form>
  );
};
