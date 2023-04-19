import { createStyles, rem } from '@mantine/core';
import { Trash, Edit } from 'tabler-icons-react';

const useStyles = createStyles((theme) => ({
  tableHandlers: {
    display: 'flex',
    justifyContent: 'flex-end',
    gap: rem(16),
    cursor: 'pointer',
  },

  handlerIcon: {
    strokeWidth: '1.5',
    color: theme.colors.dark[3],

    '&:hover': {
      color: theme.colors.secondaryColors[2],
    },
  },
}));

export const TableHandlers = () => {
  const { classes } = useStyles();

  return (
    <div className={classes.tableHandlers}>
      <Trash className={classes.handlerIcon} size={25} />
      <Edit className={classes.handlerIcon} size={25} />
    </div>
  );
};
