import { FC } from 'react';
import { createStyles } from '@mantine/core';

interface LogoProps {
  isBlack: boolean;
}

const useStyles = createStyles(() => ({
  logo: {
    height: '50px',
    width: '50px',
  },

  invertedLogo: {
    filter: `invert(100%)`,
  },
}));

export const Logo: FC<LogoProps> = ({ isBlack = false }) => {
  const { classes, cx } = useStyles();

  return (
    <img
      src='logo.png'
      alt='zayada logo'
      className={cx(classes.logo, { [classes.invertedLogo]: isBlack })}
    />
  );
};
