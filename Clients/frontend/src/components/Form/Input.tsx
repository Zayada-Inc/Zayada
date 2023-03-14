import { FC } from 'react';
import { useForm, FieldValues, UseFormRegister } from 'react-hook-form';

interface InputProps {
  name: string;
  label: string;
  type: 'text' | 'email' | 'password';
  className?: string;
  placeholder?: string;
  register: UseFormRegister<FieldValues>;
}

export const Input: FC<InputProps> = ({
  name,
  label,
  type = 'text',
  className,
  placeholder,
  register,
}) => {
  return (
    <label className='flex flex-col w-3/5'>
      {name}
      <input
        {...register(name)}
        name={name}
        aria-label={name}
        type={type}
        placeholder={placeholder}
        className={`${className} border-2`}
      />
    </label>
  );
};
