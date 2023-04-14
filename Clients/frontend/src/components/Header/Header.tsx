import { useState } from 'react';
import { useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { useDisclosure } from '@mantine/hooks';
import {
  Header as MantineHeader,
  Container,
  Text,
  Avatar,
  Burger,
  createStyles,
  Transition,
  Paper,
  rem,
} from '@mantine/core';

import { selectUser } from 'store/slices/user';
import { HEADER_LINKS } from 'components/Header/constants';

const useStyles = createStyles((theme) => ({
  header: {
    display: 'flex',
    height: '100%',
    maxWidth: 'initial',
    alignItems: 'center',
    justifyContent: 'space-between',
    padding: '0px 32px',
    backgroundColor: theme.colors.secondaryColors[1],

    [theme.fn.smallerThan('sm')]: {
      width: '100vw',
    },
  },

  welcome: {
    fontSize: theme.fontSizes.lg,
    color: theme.colors.secondaryColors[0],
  },

  user: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'space-around',
    gap: '16px',
    color: theme.colors.secondaryColors[0],
    margin: '0',

    [theme.fn.smallerThan('sm')]: {
      display: 'none',
    },
  },

  burger: {
    [theme.fn.largerThan('sm')]: {
      display: 'none',
    },
  },

  dropdown: {
    position: 'absolute',
    top: 60,
    left: 0,
    right: 0,
    zIndex: 0,
    borderTopRightRadius: 0,
    borderTopLeftRadius: 0,
    borderTopWidth: 0,
    overflow: 'hidden',
    color: theme.colors.secondaryColors[1],

    [theme.fn.largerThan('sm')]: {
      display: 'none',
    },
  },

  link: {
    display: 'block',
    padding: `${rem(8)} ${rem(12)}`,
    borderRadius: theme.radius.sm,

    '&:hover': {
      backgroundColor: theme.colors.gray[1],
    },
  },

  active: {
    '&, &:hover': {
      color: theme.colors.secondaryColors[2],
      backgroundColor: theme.colors.gray[1],
      fontWeight: 'bold',
    },
  },
}));

export const Header = () => {
  const [activeLink, setActiveLink] = useState<string>('Dashboard');
  const { photos, username } = useSelector(selectUser);
  const [opened, { toggle }] = useDisclosure(false);
  const { classes, cx } = useStyles();

  const handleLinkClick = (e: React.MouseEvent<HTMLAnchorElement>, label: string) => {
    setActiveLink(label);
  };

  const links = HEADER_LINKS.map((link, i) => (
    <Link
      key={i}
      to={link.href}
      className={cx(classes.link, { [classes.active]: activeLink === link.label })}
      onClick={(e: React.MouseEvent<HTMLAnchorElement>) => handleLinkClick(e, link.label)}
    >
      {link.label}
    </Link>
  ));

  return (
    <MantineHeader height={60}>
      <Container className={classes.header}>
        <Text className={classes.welcome}> Welcome back! </Text>
        <Container className={classes.user}>
          <Avatar src={photos[0].url} radius={'xl'}></Avatar>
          <Text> {username} </Text>
        </Container>

        <Burger
          opened={opened}
          onClick={toggle}
          className={classes.burger}
          size='sm'
          color='#FFFFFF'
        />

        <Transition transition='pop-top-right' duration={200} mounted={opened}>
          {(styles) => (
            <Paper className={classes.dropdown} withBorder style={styles}>
              {links}
            </Paper>
          )}
        </Transition>
      </Container>
    </MantineHeader>
  );
};
