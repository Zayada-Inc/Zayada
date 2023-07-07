import { Icon } from 'tabler-icons-react';
import { Button as MantineButton } from '@mantine/core';

interface ButtonProps {
  text: string;
  onClick: () => void;
  Icon?: Icon;
}

export const Button = ({ text, onClick, Icon }: ButtonProps) => {
  return (
    <MantineButton leftIcon={Icon && <Icon />} {...{ onClick }} variant='outline'>
      {text}
    </MantineButton>
  );
};
