import { useForm } from 'react-hook-form';
import { Input } from './Input';

export type FormFields = {
  [key: string]: string;
  type: 'text' | 'email' | 'password';
}[];

interface FormProps {
  fields: FormFields;
  onSubmit: (data: { [key: string]: string }) => void;
}

export const Form = ({ fields, onSubmit }: FormProps) => {
  const { register, handleSubmit } = useForm();

  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className='flex flex-col items-center border-2 border-green-500 gap-4'
    >
      {fields.map((field, i: number) => (
        <Input key={i} name={field.name} label={field.name} type={field.type} {...{ register }} />
      ))}
      {/* TO-DO: replace with btn component */}
      <input type='submit' className='w-3/5 bg-blue-600 text-white p-1' />
    </form>
  );
};
