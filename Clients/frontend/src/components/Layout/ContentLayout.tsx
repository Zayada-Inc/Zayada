import { Flex, SimpleGrid, createStyles } from '@mantine/core';

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
        <div> {children}</div>
      </Flex>
    </SimpleGrid>
  );
};
