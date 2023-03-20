import { FC } from 'react';
import { FieldValues, UseFormRegister } from 'react-hook-form';

interface InputProps {
  name: string;
  label: string;
  type: 'text' | 'email' | 'password' | 'number';
  register: UseFormRegister<FieldValues>;
  error: any;
  className?: string;
  placeholder?: string;
}

export const Input: FC<InputProps> = ({
  name,
  type = 'text',
  className,
  placeholder,
  register,
  error,
}) => {
  return (
    <>
      <label className='flex flex-col'>
        {name}
        <input
          {...register(name)}
          name={name}
          aria-label={name}
          type={type}
          placeholder={placeholder}
          className={`${className} border-2 py-1 rounded-md ${error ? 'border-red-500' : ''}`}
        />
      </label>
      {error && <p className='text-red-500 text-sm'>{error.message}</p>}
    </>
  );
};
