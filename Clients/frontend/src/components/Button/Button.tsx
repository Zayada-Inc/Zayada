interface ButtonProps {
  text: string;
}

export const Button = ({ text }: ButtonProps) => {
  return <button className='w-3/5 py-1 text-sm py bg-primary-btn'>{text}</button>;
};
