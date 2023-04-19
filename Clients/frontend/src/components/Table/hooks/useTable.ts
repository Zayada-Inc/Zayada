import { useState } from 'react';
import { createStyles } from '@mantine/core';

import { ITableItem } from 'components/Table';

const useStyles = createStyles((theme) => ({
  header: {
    backgroundColor: theme.colors.gray[2],
  },

  selectedRow: {
    backgroundColor: theme.colors.gray[0],
  },
}));

export const useTable = <T extends ITableItem>(data: T[]) => {
  const [selection, setSelection] = useState<string[]>([]);
  const { classes, cx } = useStyles();

  const toggleRow = (id: string) => {
    setSelection((current) =>
      current.includes(id) ? current.filter((item) => item !== id) : [...current, id],
    );
  };

  const toggleAll = () => {
    data &&
      setSelection((current) =>
        current.length === data.length ? [] : data.map((item) => item.id),
      );
  };

  return { classes, cx, selection, setSelection, toggleAll, toggleRow };
};
