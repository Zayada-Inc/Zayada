import { Flex, ScrollArea, SimpleGrid, createStyles } from '@mantine/core';

import { Drawer } from 'components/Drawer';
import { Header } from 'components/Header';

const useStyles = createStyles((theme) => ({
  gridContainer: {
    gap: 'initial',
    gridTemplateColumns: '70px 1fr',

    [theme.fn.smallerThan('sm')]: {
      width: '100%',
    },
  },

  scrollArea: {
    height: `calc(100vh - 60px)`,
    width: `calc(100vw - 70px)`,

    [theme.fn.smallerThan('sm')]: {
      width: '100vw',
    },
  },

  childrenWrapper: {
    display: 'flex',
    justifyContent: 'space-around',

    [theme.fn.smallerThan('sm')]: {
      flexDirection: 'column',
      alignItems: 'center',
    },
  },
}));

interface ContentLayoutProps {
  children: React.ReactNode;
}

export const ContentLayout = ({ children }: ContentLayoutProps) => {
  const { classes } = useStyles();
  return (
    <SimpleGrid cols={2} className={classes.gridContainer}>
      <Drawer />
      <Flex direction='column'>
        <Header />
        <ScrollArea className={classes.scrollArea} type='scroll'>
          <div className={classes.childrenWrapper}> {children}</div>
        </ScrollArea>
      </Flex>
    </SimpleGrid>
  );
};
