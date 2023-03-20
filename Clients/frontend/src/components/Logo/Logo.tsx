import { FC } from 'react';

interface LogoProps {
  isBlack: boolean;
}

export const Logo: FC<LogoProps> = ({ isBlack = false }) => {
  return <img src='logo.png' alt='zayada logo' className={`h-[50px] ${isBlack ? 'invert' : ''}`} />;
};
