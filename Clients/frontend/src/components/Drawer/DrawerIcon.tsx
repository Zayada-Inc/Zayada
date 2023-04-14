import { Tooltip, UnstyledButton, createStyles, rem } from '@mantine/core';
import { FC } from 'react';
import { Icon } from 'tabler-icons-react';

const useStyles = createStyles((theme) => ({
  link: {
    height: rem(50),
    width: rem(50),
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    borderRadius: theme.radius.md,
    color: theme.colors.secondaryColors[0],

    '&:hover': {
      backgroundColor: theme.colors.gray[8],
    },
  },

  active: {
    '&, &:hover': {
      backgroundColor: `${theme.colors.gray[8]} !important`,
      color: theme.colors.secondaryColors[2],
    },
  },

  icons: {
    strokeWidth: '1.5',
  },

  logoutIcon: {
    marginLeft: '7px',
  },
}));

export interface DrawerIconProps {
  icon: Icon;
  label: string;
  active?: boolean;
  onClick?: () => void;
}

export const DrawerIcon: FC<DrawerIconProps> = ({ icon: Icon, label, active, onClick }) => {
  const { classes, cx } = useStyles();

  return (
    <Tooltip label={label} position='right' transitionProps={{ duration: 0 }}>
      <UnstyledButton onClick={onClick} className={cx(classes.link, { [classes.active]: active })}>
        <Icon
          size={25}
          className={cx(classes.icons, { [classes.logoutIcon]: label === 'Logout' })}
        />
      </UnstyledButton>
    </Tooltip>
  );
};
