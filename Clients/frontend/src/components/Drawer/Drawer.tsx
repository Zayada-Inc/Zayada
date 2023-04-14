import { useState } from 'react';
import { Navbar, Stack, createStyles } from '@mantine/core';

import { Logo } from 'components/Logo';
import { DrawerIcon } from 'components/Drawer/DrawerIcon';
import { ICONS_BOTTOM, ICONS_TOP } from 'components/Drawer/constants';

const useStyles = createStyles((theme) => ({
  navbar: {
    backgroundColor: theme.colors.secondaryColors[1],
    height: '100vh',
    border: 'initial',

    [theme.fn.smallerThan('sm')]: {
      display: 'none',
    },
  },

  logoContainer: {
    height: '60px',
  },

  icons: {
    color: theme.colors.secondaryColors[0],
  },

  logout: {
    marginLeft: '7px',
  },
}));

export const Drawer = () => {
  const [activeLink, setActiveLink] = useState<number>(0);
  const { classes } = useStyles();

  const linksTop = ICONS_TOP.map((icon, i) => (
    <DrawerIcon {...icon} key={i} active={i === activeLink} onClick={() => setActiveLink(i)} />
  ));
  const linksBottom = ICONS_BOTTOM.map((icon, i) => <DrawerIcon {...icon} key={i} />);

  return (
    <Navbar className={classes.navbar}>
      <Stack align='center' justify='center' className={classes.logoContainer}>
        <Logo isBlack={false} />
      </Stack>
      <Navbar.Section grow mt={40}>
        <Stack align='center' spacing={40}>
          {linksTop}
        </Stack>
      </Navbar.Section>
      <Navbar.Section mb={40}>
        <Stack align='center' spacing={40}>
          {linksBottom}
        </Stack>
      </Navbar.Section>
    </Navbar>
  );
};
