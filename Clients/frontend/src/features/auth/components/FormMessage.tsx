import { FC } from 'react';
import { Link } from 'react-router-dom';

interface FormMessageProps {
  linkTo: string;
  message: string;
  linkedMessage: string;
}

export const FormMessage: FC<FormMessageProps> = ({ linkTo, message, linkedMessage }) => {
  return (
    <div className='flex gap-1'>
      <p>{message}</p>
      <Link to={linkTo} className='text-blue-700'>
        {linkedMessage}
      </Link>
    </div>
  );
};
